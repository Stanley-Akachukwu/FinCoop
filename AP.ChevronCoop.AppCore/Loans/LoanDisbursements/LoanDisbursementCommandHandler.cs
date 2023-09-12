using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Loans.LoanDisbursements;

public class LoanDisbursementCommandHandler :
  IRequestHandler<QueryLoanDisbursementCommand, CommandResult<IQueryable<LoanDisbursement>>>,
  IRequestHandler<CreateLoanDisbursementCommand, CommandResult<LoanDisbursementViewModel>>,
  IRequestHandler<CreateInitializeLoanOffsetCommand, CommandResult<LoanDisbursementViewModel>>,
  IRequestHandler<UpdateLoanDisbursementCommand, CommandResult<LoanDisbursementViewModel>>,
  IRequestHandler<DeleteLoanDisbursementCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly IManageApprovalService _approval;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public LoanDisbursementCommandHandler(ChevronCoopDbContext appDbContext, IManageApprovalService approval,
      ILogger<LoanDisbursementCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        _approval = approval;
        logger = _logger;
        mapper = _mapper;
    }

    public async Task<CommandResult<LoanDisbursementViewModel>> Handle(CreateLoanDisbursementCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanDisbursementViewModel>();
        // var entity = mapper.Map<LoanDisbursement>(request);
        var loanAccount = await dbContext.LoanAccounts
          .Include(y => y.LoanApplication).ThenInclude(p => p.LoanProduct)
          .FirstOrDefaultAsync(x => x.Id == request.LoanAccountId, cancellationToken: cancellationToken);

        var loanhelper = new LoanHelper(loanAccount!.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
          loanAccount.LoanApplication.LoanProduct.InterestMethod,
          loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
          loanAccount.TenureUnit, loanAccount.TenureValue,
          loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
          loanAccount.LoanApplication.LoanProduct.DaysInYear,
          loanAccount.RepaymentCommencementDate.UtcDateTime);

        var paymentSchedules = loanhelper.GetAmortizationTable(loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
        var firstPayment = paymentSchedules.FirstOrDefault();


        // Loan disbursement implementation
        var disbursement = new LoanDisbursement
        {
            Amount = request.Amount,
            Status = TransactionStatus.PENDING,
            LoanAccountId = loanAccount.Id,
            DisbursementStatus = DisbursementStatusType.PENDING,
            DisbursementDate = DateTimeOffset.Now,
            DisbursementMode = request.DisbursementMode,
            CustomerBankAccountId = loanAccount.DestinationAccountId,
            SpecialDepositAccountId = loanAccount.SpecialDepositAccountId,
            DisbursementAccountId = request.DisbursementAccountId,
            TransactionType = request.TransactionType
        };

        dbContext.LoanDisbursements.Add(disbursement);

        var productCharges = await dbContext.LoanProductCharges
          .Include(z => z.Charge)
          .Where(x =>
            x.ChargeType == ChargeType.LOAN_DISBURSEMENT && x.ProductId == loanAccount.LoanApplication.LoanProductId
          ).ToListAsync(cancellationToken: cancellationToken);

        var disbursementCharges = new List<LoanDisbursementCharge>();
        foreach (var loanProductCharge in productCharges)
        {
            decimal chargeTarget = 0;
            switch (loanProductCharge.Charge.Target)
            {
                case ChargeTarget.PRINCIPAL:
                    chargeTarget = firstPayment.Principal;
                    break;
                case ChargeTarget.PRINCIPAL_BALANCE:
                    chargeTarget = firstPayment.PrincipalBalance;
                    break;
                case ChargeTarget.INTEREST:
                    chargeTarget = paymentSchedules.Sum(x => x.PeriodInterest);
                    break;
                case ChargeTarget.INTEREST_BALANCE:
                    chargeTarget = firstPayment.InterestBalance;
                    break;

                case ChargeTarget.PRINCIPAL_PLUS_INTEREST:
                    chargeTarget = firstPayment.Principal + paymentSchedules.Sum(x => x.PeriodInterest);
                    break;

                case ChargeTarget.PRINCIPAL_BAL_PLUS_INTEREST_BAL:
                    chargeTarget = firstPayment.Principal + firstPayment.InterestBalance;
                    break;

                case ChargeTarget.PERIOD_PRINCIPAL:
                    chargeTarget = firstPayment.PeriodPrincipal;
                    break;

                case ChargeTarget.PERIOD_INTEREST:
                    chargeTarget = firstPayment.PeriodInterest;
                    break;

                case ChargeTarget.PERIOD_PAYMENT:
                    chargeTarget = firstPayment.PeriodPayment;
                    break;
            }


            disbursementCharges.Add(new LoanDisbursementCharge()
            {
                ChargeType = loanProductCharge.ChargeType,
                //LoanDisbursementId = disbursement.DisbursementAccountId,
                DisbursementChargeId = loanProductCharge.ChargeId,
                TotalCharge = loanProductCharge.Charge.CalculateCharge(chargeTarget)
            });
        }

        //await dbContext.LoanDisbursementCharges.AddRangeAsync(disbursementCharges, cancellationToken);
        disbursement.LoanDisbursementCharges.AddRange(disbursementCharges);

        await dbContext.SaveChangesAsync(cancellationToken);

        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == loanAccount.CustomerId, cancellationToken: cancellationToken);

        var approvalRequest = new CreateApprovalModel();

        if (request.TransactionType == TransactionType.LOAN_DISBURSEMENT_TOPUP)
        {
            approvalRequest = new CreateApprovalModel
            {
                Module = "LoanTopupDisbursement",
                Payload = JsonConvert.SerializeObject(disbursement.Id),
                Comment = "Create loan topup disbursement approval initiated",
                ApprovalType = ApprovalType.LOAN_DISBURSEMENT_TOPUP,
                Description =
            $"Loan Disbursement Topup - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({loanAccount.AccountNo})",
                CreatedBy = request.CreatedByUserId,
                EntityId = disbursement!.Id,
                EntityType = typeof(CreateLoanDisbursementCommand)
            };
        }
        else
        {
            approvalRequest = new CreateApprovalModel
            {
                Module = "LoanDisbursement",
                Payload = JsonConvert.SerializeObject(disbursement.Id),
                Comment = "Create loan disbursement approval initiated",
                ApprovalType = ApprovalType.LOAN_DISBURSEMENT,
                Description =
            $"Loan Disbursement - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({loanAccount.AccountNo})",
                CreatedBy = request.CreatedByUserId,
                EntityId = disbursement!.Id,
                EntityType = typeof(CreateLoanDisbursementCommand)
            };
        }

        var approval = await _approval.CreateApproval(approvalRequest, true);

        if (approval.StatusCode == StatusCodes.Status201Created)
        {
            disbursement.ApprovalId = approval.Response.Id;
            dbContext.LoanDisbursements.Update(disbursement);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<LoanDisbursementViewModel>(disbursement);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanDisbursementCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.LoanDisbursements.FindAsync(request.Id);

        dbContext.LoanDisbursements.Remove(entity!);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanDisbursement>>> Handle(QueryLoanDisbursementCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanDisbursement>>();
        rsp.Response = dbContext.LoanDisbursements;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanDisbursementViewModel>> Handle(UpdateLoanDisbursementCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanDisbursementViewModel>();
        var entity = await dbContext.LoanDisbursements.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.LoanDisbursements.Update(entity!);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<LoanDisbursementViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<LoanDisbursementViewModel>> Handle(CreateInitializeLoanOffsetCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanDisbursementViewModel>();
        // var entity = mapper.Map<LoanDisbursement>(request);
        var loanAccount = await dbContext.LoanAccounts
          .Include(y => y.LoanApplication).ThenInclude(p => p.LoanProduct)
          .FirstOrDefaultAsync(x => x.Id == request.LoanAccountId, cancellationToken: cancellationToken);

        var loanhelper = new LoanHelper(loanAccount.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
          loanAccount.LoanApplication.LoanProduct.InterestMethod,
          loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
          loanAccount.TenureUnit, loanAccount.TenureValue,
          loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
          loanAccount.LoanApplication.LoanProduct.DaysInYear,
          loanAccount.RepaymentCommencementDate.UtcDateTime);

        var paymentSchedules = loanhelper.GetAmortizationTable(loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
        var firstPayment = paymentSchedules.FirstOrDefault();


        // Loan disbursement implementation
        var disbursement = new LoanDisbursement
        {
            Amount = request.Amount,
            Status = TransactionStatus.PENDING,
            LoanAccountId = loanAccount.Id,
            DisbursementStatus = DisbursementStatusType.PENDING,
            DisbursementDate = DateTimeOffset.Now,
            DisbursementMode = LoanDisbursementMode.INITIALIZED,
            TransactionType = request.TransactionType
        };

        dbContext.LoanDisbursements.Add(disbursement);

        var productCharges = await dbContext.LoanProductCharges
          .Include(z => z.Charge)
          .Where(x =>
            x.ChargeType == ChargeType.LOAN_DISBURSEMENT && x.ProductId == loanAccount.LoanApplication.LoanProductId
          ).ToListAsync(cancellationToken: cancellationToken);

        var disbursementCharges = new List<LoanDisbursementCharge>();
        foreach (var loanProductCharge in productCharges)
        {
            decimal chargeTarget = 0;
            switch (loanProductCharge.Charge.Target)
            {
                case ChargeTarget.PRINCIPAL:
                    chargeTarget = firstPayment.Principal;
                    break;
                case ChargeTarget.PRINCIPAL_BALANCE:
                    chargeTarget = firstPayment.PrincipalBalance;
                    break;
                case ChargeTarget.INTEREST:
                    chargeTarget = paymentSchedules.Sum(x => x.PeriodInterest);
                    break;
                case ChargeTarget.INTEREST_BALANCE:
                    chargeTarget = firstPayment.InterestBalance;
                    break;

                case ChargeTarget.PRINCIPAL_PLUS_INTEREST:
                    chargeTarget = firstPayment.Principal + paymentSchedules.Sum(x => x.PeriodInterest);
                    break;

                case ChargeTarget.PRINCIPAL_BAL_PLUS_INTEREST_BAL:
                    chargeTarget = firstPayment.Principal + firstPayment.InterestBalance;
                    break;

                case ChargeTarget.PERIOD_PRINCIPAL:
                    chargeTarget = firstPayment.PeriodPrincipal;
                    break;

                case ChargeTarget.PERIOD_INTEREST:
                    chargeTarget = firstPayment.PeriodInterest;
                    break;

                case ChargeTarget.PERIOD_PAYMENT:
                    chargeTarget = firstPayment.PeriodPayment;
                    break;
                
                case ChargeTarget.VALUE:
                    chargeTarget = firstPayment.Principal;
                    break;
            }


            disbursementCharges.Add(new LoanDisbursementCharge()
            {
                ChargeType = loanProductCharge.ChargeType,
                //LoanDisbursementId = disbursement.DisbursementAccountId,
                DisbursementChargeId = loanProductCharge.ChargeId,
                TotalCharge = loanProductCharge.Charge.CalculateCharge(chargeTarget)
            });
        }

        //await dbContext.LoanDisbursementCharges.AddRangeAsync(disbursementCharges, cancellationToken);
        disbursement.LoanDisbursementCharges.AddRange(disbursementCharges);

        await dbContext.SaveChangesAsync(cancellationToken);

        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == loanAccount.CustomerId, cancellationToken: cancellationToken);

        var approvalRequest = new CreateApprovalModel
        {
            Module = "LoanDisbursement",
            Payload = JsonConvert.SerializeObject(disbursement.Id),
            Comment = "Create loan disbursement approval initiated",
            ApprovalType = ApprovalType.LOAN_DISBURSEMENT,
            Description =
            $"Loan Disbursement - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({loanAccount.AccountNo})",
            CreatedBy = request.CreatedByUserId,
            EntityId = disbursement!.Id,
            EntityType = typeof(CreateLoanDisbursementCommand)
        };

        var approval = await _approval.CreateApproval(approvalRequest, true);

        if (approval.StatusCode == StatusCodes.Status201Created)
        {
            disbursement.ApprovalId = approval.Response.Id;
            dbContext.LoanDisbursements.Update(disbursement);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<LoanDisbursementViewModel>(disbursement);

        return rsp;
    }
}