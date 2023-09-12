using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;
using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.AppDomain.Loans.LoanTransactions;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffsets;

public class LoanOffsetCommandHandler :
  IRequestHandler<QueryLoanOffsetCommand, CommandResult<IQueryable<LoanOffset>>>,
  IRequestHandler<CreateLoanOffsetCommand, CommandResult<LoanOffsetViewModel>>,
  IRequestHandler<ProcessLoanOffsetCommand, CommandResult<LoanOffsetViewModel>>,
  IRequestHandler<UpdateLoanOffsetCommand, CommandResult<LoanOffsetViewModel>>,
  IRequestHandler<DeleteLoanOffsetCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IManageApprovalService _approval;

    public LoanOffsetCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator,
      ILogger<LoanOffsetCommandHandler> logger, IMapper mapper, IManageApprovalService approval)
    {
        _dbContext = appDbContext;
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
        _approval = approval;

    }

    public async Task<CommandResult<LoanOffsetViewModel>> Handle(CreateLoanOffsetCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanOffsetViewModel>();
        var entity = _mapper.Map<LoanOffset>(request);

        _dbContext.LoanOffsets.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var loanAccount = await _dbContext.LoanAccounts
            .Include(x => x.LoanApplication.LoanProduct)
            .Include(x => x.PrincipalBalanceAccount)
            .Include(x => x.InterestBalanceAccount)
            .FirstOrDefaultAsync(c => c.Id == request.LoanAccountId, cancellationToken);

        entity.OldPrincipalBalance = loanAccount!.PrincipalBalanceAccount.LedgerBalance;
        entity.OldInterestBalance = loanAccount!.InterestBalanceAccount.LedgerBalance;
        entity.NewPrincipalBalance = entity.OldPrincipalBalance - request.OffsetAmount;

        var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == loanAccount!.CustomerId, cancellationToken);

        var approvalRequest = new CreateApprovalModel
        {
            Module = "LoanOffsetApplication",
            Payload = JsonConvert.SerializeObject(request),
            Comment = "Create loan offset approval initiated",
            ApprovalType = ApprovalType.LOAN_OFFSET_APPLICATION,
            Description = $"Loan Offset Application - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({loanAccount!.LoanApplication?.ApplicationNo})",
            CreatedBy = request.CreatedByUserId,
            EntityId = entity.Id,
            EntityType = typeof(CreateLoanTopupCommand)
        };

        var approval = await _approval.CreateApproval(approvalRequest, true);

        if (approval.StatusCode == StatusCodes.Status201Created)
        {
            entity.ApprovalId = approval.Response.Id;
            _dbContext.LoanOffsets.Update(entity);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        rsp.Response = _mapper.Map<LoanOffsetViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanOffsetCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await _dbContext.LoanOffsets.FindAsync(request.Id);

        _dbContext.LoanOffsets.Remove(entity!);
        await _dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanOffset>>> Handle(QueryLoanOffsetCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanOffset>>();
        rsp.Response = _dbContext.LoanOffsets;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanOffsetViewModel>> Handle(UpdateLoanOffsetCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanOffsetViewModel>();
        var entity = await _dbContext.LoanOffsets.FindAsync(request.Id);

        _mapper.Map(request, entity);

        _dbContext.LoanOffsets.Update(entity!);
        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = _mapper.Map<LoanOffsetViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<LoanOffsetViewModel>> Handle(ProcessLoanOffsetCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanOffsetViewModel>();
        try
        {
            var entity = await _dbContext.LoanOffsets.Include(x => x.Approval).FirstOrDefaultAsync(x => x.Id == request.LoanOffsetId, cancellationToken);
            entity!.Approval!.Status = request.Status;
            entity.Approval.DateUpdated = DateTime.UtcNow.ToLocalTime();
            entity.DateUpdated = DateTime.UtcNow.ToLocalTime();

            _dbContext.LoanOffsets.Update(entity);

            //var chargesAccount = new LoanAccount();

            var oldLoanAccount = await _dbContext.LoanAccounts
                .Include(x => x.LoanApplication)
                .ThenInclude(y => y.LoanProduct)
                .FirstOrDefaultAsync(x => x.Id == entity!.LoanAccountId, cancellationToken: cancellationToken);

            if (request.Status == ApprovalStatus.APPROVED)
            {
                if (entity.AllowedOffsetType == AllowedOffsetType.IN_LIEU_OF_PAYROLL)
                {
                    // Update all loan repayment schedule in request.repaymentSchedules ispaid status to true
                    var repaymentSchedule = await _dbContext.LoanRepaymentSchedules.Where(x => entity.RepaymentSchedules.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);
                    foreach (var schedule in repaymentSchedule)
                    {
                        schedule.IsPaid = true;
                    }
                }

                if (entity.AllowedOffsetType == AllowedOffsetType.PARTIAL)
                {
                    var currency = oldLoanAccount!.LoanApplication!.LoanProduct.DefaultCurrencyId;

                    var accountNo = NHiloHelper.GetNextKey(nameof(LoanAccount)).ToString();
                    var newLoanAccount = new LoanAccount
                    {
                        AccountNo = accountNo,
                        LoanApplicationId = oldLoanAccount?.LoanApplicationId,
                        Principal = entity.NewPrincipalBalance,
                        TenureUnit = oldLoanAccount!.TenureUnit,
                        TenureValue = oldLoanAccount!.TenureValue,
                        SpecialDepositAccountId = oldLoanAccount.SpecialDepositAccountId,
                        UseSpecialDeposit = oldLoanAccount.UseSpecialDeposit,
                        CustomerId = oldLoanAccount.CustomerId,
                        DestinationAccountId = oldLoanAccount.DestinationAccountId
                    };

                    if (!string.IsNullOrEmpty(oldLoanAccount.RootParentAccountId))
                        newLoanAccount.RootParentAccountId = oldLoanAccount.RootParentAccountId;
                    else
                        newLoanAccount.RootParentAccountId = oldLoanAccount.Id;

                    newLoanAccount.ParentAccountId = oldLoanAccount.Id;
                    newLoanAccount.LoanCreationType = LoanCreationType.OFFSET;

                    entity!.LoanAccountId = newLoanAccount.Id;

                    // Create Ledger Accounts
                    var PrincipalBalanceAccount = new LedgerAccount
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Principal Balance Account ({accountNo})",
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL,
                        AllowManualEntry = true,
                        CurrencyId = currency
                    };
                    _dbContext.LedgerAccounts.Add(PrincipalBalanceAccount);
                    newLoanAccount.PrincipalBalanceAccount = PrincipalBalanceAccount;

                    var PrincipalLossAccount = new LedgerAccount
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Principal Loss Account ({accountNo})",
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL,
                        AllowManualEntry = true,
                        CurrencyId = currency
                    };
                    _dbContext.LedgerAccounts.Add(PrincipalLossAccount);
                    newLoanAccount.PrincipalLossAccount = PrincipalLossAccount;

                    var EarnedInterestAccount = new LedgerAccount
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Earned Interest Account ({accountNo})",
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL,
                        AllowManualEntry = true,
                        CurrencyId = currency
                    };
                    _dbContext.LedgerAccounts.Add(EarnedInterestAccount);
                    newLoanAccount.EarnedInterestAccount = EarnedInterestAccount;

                    var InterestBalanceAccount = new LedgerAccount()
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Interest Balance Account ({accountNo})",
                        CurrencyId = currency,
                        AllowManualEntry = true,
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL
                    };
                    _dbContext.LedgerAccounts.Add(InterestBalanceAccount);
                    newLoanAccount.InterestBalanceAccount = InterestBalanceAccount;

                    var UnearnedInterestAccount = new LedgerAccount
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Unearned Interest Account ({accountNo})",
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL,
                        AllowManualEntry = true,
                        CurrencyId = currency
                    };
                    _dbContext.LedgerAccounts.Add(UnearnedInterestAccount);
                    newLoanAccount.UnearnedInterestAccount = UnearnedInterestAccount;

                    var InterestLossAccount = new LedgerAccount
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Interest Loss Account ({accountNo})",
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL,
                        AllowManualEntry = true,
                        CurrencyId = currency
                    };
                    _dbContext.LedgerAccounts.Add(InterestLossAccount);
                    newLoanAccount.InterestLossAccount = InterestLossAccount;

                    var InterestWaivedAccount = new LedgerAccount
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Interest Waived Account ({accountNo})",
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL,
                        AllowManualEntry = true,
                        CurrencyId = currency
                    };
                    _dbContext.LedgerAccounts.Add(InterestWaivedAccount);
                    newLoanAccount.InterestWaivedAccount = InterestWaivedAccount;

                    var ChargesWaivedAccount = new LedgerAccount
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Charges Waived Account ({accountNo})",
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL,
                        AllowManualEntry = true,
                        CurrencyId = currency
                    };
                    _dbContext.LedgerAccounts.Add(ChargesWaivedAccount);
                    newLoanAccount.ChargesWaivedAccount = ChargesWaivedAccount;

                    var chargesAccruedAccount = new LedgerAccount()
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Charges Accrued Account ({accountNo})",
                        CurrencyId = currency,
                        AllowManualEntry = true,
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL
                    };
                    _dbContext.LedgerAccounts.Add(chargesAccruedAccount);
                    newLoanAccount.ChargesAccruedAccount = chargesAccruedAccount;

                    var chargesIncomeAccount = new LedgerAccount()
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Charges Income Account ({accountNo})",
                        CurrencyId = currency,
                        AllowManualEntry = true,
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL
                    };
                    _dbContext.LedgerAccounts.Add(chargesIncomeAccount);
                    newLoanAccount.ChargesIncomeAccount = chargesIncomeAccount;

                    var InterestEarnedAccount = new LedgerAccount()
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Interest Earned Account ({accountNo})",
                        CurrencyId = currency,
                        AllowManualEntry = true,
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL
                    };
                    _dbContext.LedgerAccounts.Add(InterestEarnedAccount);
                    newLoanAccount.EarnedInterestAccount = InterestEarnedAccount;

                    var InterestPayoutAccount = new LedgerAccount
                    {
                        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
                        Name = $"Interest Payout Account ({accountNo})",
                        IsOfficeAccount = true,
                        AccountType = COAType.CONTROL,
                        AllowManualEntry = true,
                        CurrencyId = currency
                    };
                    _dbContext.LedgerAccounts.Add(InterestPayoutAccount);
                    newLoanAccount.InterestPayoutAccount = InterestPayoutAccount;

                    _dbContext.LoanAccounts.Add(newLoanAccount);

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    var loanAccount = await _dbContext.LoanAccounts
                        .Include(x => x.LoanApplication).ThenInclude(y => y.LoanProduct)
                        .FirstOrDefaultAsync(x => x.Id == newLoanAccount.Id, cancellationToken: cancellationToken);

                    var loanProduct = loanAccount!.LoanApplication.LoanProduct;

                    var oldLoanRepaymentSchedules = _dbContext.LoanRepaymentSchedules.Where(x =>
                        x.LoanAccountId == oldLoanAccount.Id &&
                        !x.IsPaid).OrderBy(x => x.DateCreated);

                    var monthlyPeriodPrincipal = oldLoanRepaymentSchedules!.FirstOrDefault()!.PeriodPrincipal;
                    var loanTenure = oldLoanRepaymentSchedules!.FirstOrDefault()!.TenureValue;

                    var totalOffsetPeriod = entity.OffsetAmount / monthlyPeriodPrincipal;
                    var remainder = entity.OffsetAmount % monthlyPeriodPrincipal;

                    var fullyRepaymentPeriod = Convert.ToInt32(Math.Floor(totalOffsetPeriod));
                    
                    var repaymentPeriods = await oldLoanRepaymentSchedules.Take(fullyRepaymentPeriod).ToListAsync(cancellationToken: cancellationToken);
                    foreach (var period in repaymentPeriods)
                    {
                        period.IsPaid = true;
                    }

                    var lastPaymentDate = oldLoanRepaymentSchedules!
                        .OrderBy(x => x.DateCreated)!
                        .Skip(fullyRepaymentPeriod)!
                        .First().PeriodStartDate;

                    var newLoanRepaymentPeriod =
                        Convert.ToDecimal(oldLoanRepaymentSchedules!.Count() - fullyRepaymentPeriod);
                    loanAccount!.TenureValue = newLoanRepaymentPeriod;
                    loanAccount.RepaymentCommencementDate = lastPaymentDate;

                    var loan = new LoanHelper(newLoanAccount!.Principal, loanProduct.InterestRate,
                        loanProduct.InterestMethod, loanProduct.InterestCalculationMethod,
                        loanProduct.TenureUnit, newLoanRepaymentPeriod,
                        loanProduct.RepaymentPeriod, loanProduct.DaysInYear,
                        loanAccount.RepaymentCommencementDate.DateTime);

                    List<AmortizationSchedule> loanSchedules =
                        loan.GetAmortizationTable(loan.InterestCalculationMethod);

                    var batchRef = NHiloHelper.GetNextKey(nameof(LoanRepaymentSchedule)).ToString();

                    var loanRepaymentSchedules = _mapper.Map<List<LoanRepaymentSchedule>>(loanSchedules);
                    foreach (var schedule in loanRepaymentSchedules)
                    {
                        schedule.BatchRefNo = batchRef;
                        schedule.LoanAccountId = newLoanAccount.Id;
                        schedule.IsPrincipalAllocated = true;
                        schedule.IsInterestAllocated = true;
                    }
                    _dbContext.LoanRepaymentSchedules.AddRange(loanRepaymentSchedules);
                    
                    // Set loan offset charges
                    if (oldLoanAccount!.LoanApplication.LoanProduct.EnableAdminOffsetCharge)
                    {
                        var charges = GetLoanOffsetCharges(entity, loanAccount);
                        _dbContext.LoanOffSetCharges.AddRange(charges);
                    }
                }

                if (entity.AllowedOffsetType == AllowedOffsetType.FULL)
                {
                    var repayments = await _dbContext.LoanRepaymentSchedules.Where(x => !x.IsPaid && x.LoanAccountId == oldLoanAccount.Id).ToListAsync(cancellationToken: cancellationToken);
                    foreach (var repayment in repayments)
                    {
                        repayment.IsPaid = true;
                    }  
                    
                    // Set loan offset charges
                    if (oldLoanAccount!.LoanApplication.LoanProduct.EnableAdminOffsetCharge)
                    {
                        var charges = GetLoanOffsetCharges(entity, oldLoanAccount);
                        _dbContext.LoanOffSetCharges.AddRange(charges);
                    }
                }


                // Offset transaction
                var transaction = new LoanTransactionCommand()
                {
                    EntityId = entity.Id,
                    EntityType = typeof(LoanOffset),
                    IsApproved = true,
                    ApprovedOn = DateTime.Now,
                    TransactionType = TransactionType.LOAN_OFFSET_TRANSFER,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    LoanAccountId = entity.LoanAccountId,
                    TransactionJournalId = null
                };

                switch (entity.LoanRepaymentMode)
                {
                    case LoanRepaymentMode.SAVINGS:
                    {
                        transaction.TransactionType = TransactionType.LOAN_OFFSET_SAVINGS;
                        break;
                    }
                    case LoanRepaymentMode.SPECIAL_DEPOSIT:
                    {
                        transaction.TransactionType = TransactionType.LOAN_OFFSET_SPECIAL_DEPOSIT;
                        break;
                    }
                    case LoanRepaymentMode.BANK_TRANSFER:
                    {
                        transaction.TransactionType = TransactionType.LOAN_OFFSET_TRANSFER;
                        break;
                    }
                }

                var transactionResponse = await _mediator.Send(transaction, cancellationToken);
                
                if (entity.AllowedOffsetType == AllowedOffsetType.PARTIAL)
                {
                    var _disbursement = new CreateInitializeLoanOffsetCommand
                    {
                        TransactionType = TransactionType.LOAN_INITIALIZE_OFFSET,
                        Amount = entity.OffsetAmount,
                        LoanAccountId = entity.LoanAccountId,
                        DisbursementDate = DateTimeOffset.Now,
                        CreatedByUserId = entity.CreatedByUserId
                    };

                    _ = await _mediator.Send(_disbursement, cancellationToken);
                }
            }
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = _mapper.Map<LoanOffsetViewModel>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger.LogError(e.Message, e);
        }

        return rsp;
    }
    
    
    private List<LoanOffSetCharge> GetLoanOffsetCharges(LoanOffset entity, LoanAccount account)
    {
        var loanOffsetCharges = _dbContext.LoanProductCharges
            .Include(x => x.Charge)
            .Where(x => x.ChargeType == ChargeType.LOAN_ADMIN_OFFSET &&
                        x.ProductId == account.LoanApplication!.LoanProductId).AsNoTracking();

        var loanhelper = new LoanHelper(account.Principal,
            account.LoanApplication.LoanProduct.InterestRate,
            account.LoanApplication.LoanProduct.InterestMethod,
            account.LoanApplication.LoanProduct.InterestCalculationMethod,
            account.TenureUnit, account.TenureValue,
            account.LoanApplication.LoanProduct.RepaymentPeriod,
            account.LoanApplication.LoanProduct.DaysInYear,
            account.RepaymentCommencementDate.UtcDateTime);

        var paymentSchedules =
            loanhelper.GetAmortizationTable(account.LoanApplication.LoanProduct.InterestCalculationMethod);
        var firstPayment = paymentSchedules.FirstOrDefault();

        var adminOffsetCharges = new List<LoanOffSetCharge>();

        foreach (var adminOffsetCharge in loanOffsetCharges)
        {
            decimal chargeTarget = 0;
            switch (adminOffsetCharge.Charge.Target)
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

            adminOffsetCharges.Add(new LoanOffSetCharge
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                OffsetChargeId = adminOffsetCharge.ChargeId,
                TotalCharge = adminOffsetCharge.Charge.CalculateCharge(chargeTarget),
                LoanOffsetId = entity.Id,
                ChargeType = ChargeType.LOAN_ADMIN_OFFSET
            });
        }
        
        
        return adminOffsetCharges;
    }
}