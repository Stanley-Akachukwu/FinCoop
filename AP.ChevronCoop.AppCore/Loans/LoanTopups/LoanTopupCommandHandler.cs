using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using Microsoft.Extensions.Options;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Loans.LoanTransactions;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;
using System.Net;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopups;

public class LoanTopupCommandHandler :
  IRequestHandler<QueryLoanTopupCommand, CommandResult<IQueryable<LoanTopup>>>,
  IRequestHandler<CreateLoanTopupCommand, CommandResult<LoanTopupViewModel>>,
  IRequestHandler<ProcessLoanTopupCommand, CommandResult<LoanTopupViewModel>>,
  IRequestHandler<UpdateLoanTopupCommand, CommandResult<LoanTopupViewModel>>,
  IRequestHandler<DeleteLoanTopupCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IMediator _mediator;
    private readonly IManageApprovalService _approval;
    private readonly CoreAppSettings _options;

    public LoanTopupCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<LoanTopupCommandHandler> logger, IMapper mapper, IEmailService emailService,
      IOptions<CoreAppSettings> options, IMediator mediator, IManageApprovalService approval)
    {
        _dbContext = appDbContext;
        _logger = logger;
        _mapper = mapper;
        _emailService = emailService;
        _mediator = mediator;
        _options = options.Value;
        _approval = approval;

    }

    public async Task<CommandResult<LoanTopupViewModel>> Handle(CreateLoanTopupCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanTopupViewModel>();
        var entity = _mapper.Map<LoanTopup>(request);

        var loanAccount = await _dbContext.LoanAccounts
            .Include(x => x.LoanApplication.Customer)
            .Include(x => x.PrincipalBalanceAccount)
            .Include(x => x.UnearnedInterestAccount)
            .FirstOrDefaultAsync(x => x.Id == request.LoanAccountId, cancellationToken);

        entity.OldPrincipalBalance = loanAccount!.PrincipalBalanceAccount.LedgerBalance;
        entity.OldInterestBalance = loanAccount!.UnearnedInterestAccount.LedgerBalance;
        entity.NewPrincipalBalance = entity.OldPrincipalBalance + request.TopupAmount;

        _dbContext.LoanTopups.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        

        _logger.LogInformation("mediatr->CreateLoanTopupCommand->checking guarantors");

        if (request.Guarantors != null && request.Guarantors.Any())
        {
            _logger.LogInformation("mediatr->CreateLoanTopupCommand->start checking guarantors");
         
            var guarantorIds = request.Guarantors.Select(x => x.GuarantorCustomerId);

            var guarantorProfiles = await _dbContext.Customers.Where(x => guarantorIds.Contains(x.Id))
              .ToListAsync(cancellationToken);

            var loanGuarantors = new List<LoanApplicationGuarantor>();
            foreach (var profile in guarantorProfiles)
            {
                // get guarantor type
                var guarantorType = request.Guarantors.FirstOrDefault(x => x.GuarantorCustomerId == profile.Id)?.GuarantorType;

                if (guarantorType != null)
                    loanGuarantors.Add(new LoanApplicationGuarantor
                    {
                        LoanApplicationId = loanAccount!.LoanApplicationId,
                        GuarantorType = (GuarantorType)guarantorType,
                        GuarantorId = profile.Id,
                        Status = ApprovalStatus.PENDING_APPROVAL,
                        GuarantorApprovalType = GuarantorApprovalType.LOAN_TOPUP
                    }
                    );

                // TODO: Send email to guarantors
                var props = new GeneralEmailDto
                {
                    Link = $"{_options.WebBaseUrl}",
                    Name = $"{profile.FirstName} {profile.LastName}"
                };

                _ = _emailService.SendEmailAsync(EmailTemplateType.GUARANTOR_APPROVAL, profile?.PrimaryEmail, props);
            }

            _logger.LogInformation("mediatr->CreateLoanTopupCommand->update db guarantors");

            await _dbContext.LoanApplicationGuarantors.AddRangeAsync(loanGuarantors, cancellationToken);
        }
        else
        {

            var approvalRequest = new CreateApprovalModel
            {
                Module = "LoanTopupApplication",
                Payload = JsonConvert.SerializeObject(request),
                Comment = "Create loan topup approval initiated",
                ApprovalType = ApprovalType.LOAN_TOPUP_APPLICATION,
                Description = $"Loan Topup Application - {loanAccount?.LoanApplication?.Customer?.FirstName} {loanAccount?.LoanApplication?.Customer?.MiddleName} {loanAccount?.LoanApplication?.Customer?.LastName} ({loanAccount?.LoanApplication?.ApplicationNo})",
                CreatedBy = loanAccount?.CreatedByUserId,
                EntityId = entity?.Id,
                EntityType = typeof(CreateLoanTopupCommand)
            };

            _logger.LogInformation("mediatr->CreateLoanTopupCommand->start post approval");

            var approval = await _approval.CreateApproval(approvalRequest, false, loanAccount?.LoanApplication!.LoanProduct!.ApprovalWorkflowId);
            _logger.LogInformation("mediatr->CreateLoanTopupCommand->end post approval");

            if (approval.StatusCode == StatusCodes.Status201Created)
            {
                //var loanTopup = await _dbContext.LoanTopups.FirstOrDefaultAsync(x => x.LoanAccountId == loanAccount!.Id);
                entity!.ApprovalId = approval.Response.Id;
                _dbContext.LoanTopups.Update(entity);

                _logger.LogInformation("mediatr->CreateLoanTopupCommand->end update topup after approval");

            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("mediatr->CreateLoanTopupCommand->save all changes");

        rsp.Response = _mapper.Map<LoanTopupViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanTopupCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await _dbContext.LoanTopups.FindAsync(request.Id);

        _dbContext.LoanTopups.Remove(entity!);
        await _dbContext.SaveChangesAsync();
        rsp.Response = "Data successfully deleted";
        return rsp;
    }


    public Task<CommandResult<IQueryable<LoanTopup>>> Handle(QueryLoanTopupCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanTopup>>();
        rsp.Response = _dbContext.LoanTopups;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanTopupViewModel>> Handle(UpdateLoanTopupCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanTopupViewModel>();
        var entity = await _dbContext.LoanTopups.FindAsync(request.Id);

        _mapper.Map(request, entity);

        _dbContext.LoanTopups.Update(entity!);
        await _dbContext.SaveChangesAsync();

        rsp.Response = _mapper.Map<LoanTopupViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<LoanTopupViewModel>> Handle(ProcessLoanTopupCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanTopupViewModel>();
        var entity = await _dbContext.LoanTopups.Include(x => x.Approval)
            .FirstOrDefaultAsync(x => x.Id == request.LoanTopupId, cancellationToken);

        if (request.Status == ApprovalStatus.APPROVED)
        {
            var oldLoanAccount = await _dbContext.LoanAccounts
            .Include(x => x.LoanApplication.LoanProduct)
            .FirstOrDefaultAsync(x => x.Id == entity!.LoanAccountId, cancellationToken: cancellationToken);

            var currency = oldLoanAccount!.LoanApplication!.LoanProduct.DefaultCurrencyId;
            var accountNo = NHiloHelper.GetNextKey(nameof(LoanAccount)).ToString();

            var newLoanAccount = new LoanAccount
            {
                AccountNo = accountNo,
                LoanApplicationId = oldLoanAccount?.LoanApplicationId,
                Principal = entity!.NewPrincipalBalance,
                TenureUnit = oldLoanAccount!.TenureUnit,
                TenureValue = oldLoanAccount!.TenureValue,
                SpecialDepositAccountId = oldLoanAccount.SpecialDepositAccountId,
                UseSpecialDeposit = oldLoanAccount.UseSpecialDeposit,
                CustomerId = oldLoanAccount.CustomerId,
                LoanTopupId = request.LoanTopupId
            };

            if (entity.DestinationType == TopupFundingSourceType.EXISTING_BANK_ACCOUNT)
                newLoanAccount.DestinationAccountId = entity.CustomerBankAccountId;
            if (entity.DestinationType == TopupFundingSourceType.SPECIAL_DEPOSIT_ACCOUNT)
                newLoanAccount.SpecialDepositAccountId = entity.SpecialDepositAccountId;

            if (!string.IsNullOrEmpty(oldLoanAccount.RootParentAccountId))
                newLoanAccount.RootParentAccountId = oldLoanAccount.RootParentAccountId;
            else
                newLoanAccount.RootParentAccountId = oldLoanAccount.Id;

            newLoanAccount.ParentAccountId = oldLoanAccount.Id;
            newLoanAccount.LoanCreationType = LoanCreationType.TOPUP;

            // Create Ledger Accounts
            var PrincipalBalanceAccount = new LedgerAccount
            {
                Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(), //$"LPPRA-{accountNo}";
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

            oldLoanAccount!.IsClosed = true;
            oldLoanAccount.DateClosed = DateTime.UtcNow.ToLocalTime();
            entity.LoanAccountId = newLoanAccount.Id;
            _dbContext.LoanAccounts.Update(oldLoanAccount);

            _logger.LogInformation("mediatr->ProcessLoanTopupCommand->start savechanges");

            // Set loan topup charges
            if (oldLoanAccount!.LoanApplication.LoanProduct.EnableTopUpCharges)
            {
                var charges = GetLoanTopupCharges(entity, newLoanAccount);
                _dbContext.LoanTopupCharges.AddRange(charges);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("mediatr->ProcessLoanTopupCommand->end savechanges");

            var transaction = new LoanTransactionCommand()
            {
                EntityId = entity.Id,
                EntityType = typeof(LoanTopup),
                IsApproved = false,
                ApprovedOn = DateTime.Now,
                TransactionAction = TransactionAction.POST,
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.LOAN_TOPUP,
                LoanAccountId = entity.LoanAccountId,
                OldLoanAccountId = oldLoanAccount.Id,
                TransactionJournalId = null
            };

            _logger.LogInformation("mediatr->ProcessLoanTopupCommand->start trigger loan topup");

            _ = await _mediator.Send(transaction, cancellationToken);

            _logger.LogInformation("mediatr->ProcessLoanTopupCommand->end trigger loan topup");


            var applicant = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == oldLoanAccount.CustomerId, cancellationToken: cancellationToken);
            var props = new GeneralEmailDto
            {
                Link = $"{_options.WebBaseUrl}",
                Name = $"{applicant!.FirstName} {applicant.LastName}"
            };
            _ = _emailService.SendEmailAsync(EmailTemplateType.LOAN_APPROVAL, applicant!.PrimaryEmail, props);

            var loanAccount = await _dbContext.LoanAccounts
                    .Include(x => x.LoanApplication).ThenInclude(y => y.LoanProduct)
                    .FirstOrDefaultAsync(x => x.Id == newLoanAccount.Id, cancellationToken: cancellationToken);

            var loanProduct = loanAccount!.LoanApplication.LoanProduct;

            var oldLoanRepaymentSchedules = _dbContext.LoanRepaymentSchedules.AsNoTracking().Where(x => x.LoanAccountId == oldLoanAccount.Id
            && !x.IsPaid).OrderBy(x => x.DateCreated);

            var newLoanRepaymentPeriod = Convert.ToDecimal(oldLoanRepaymentSchedules!.Count());
            loanAccount!.TenureValue = newLoanRepaymentPeriod;
            loanAccount.RepaymentCommencementDate = oldLoanRepaymentSchedules.First().PeriodStartDate;

            var loan = new LoanHelper(loanAccount!.Principal, loanProduct!.InterestRate,
                loanProduct.InterestMethod, loanProduct.InterestCalculationMethod,
                loanProduct.TenureUnit, newLoanRepaymentPeriod, loanProduct.RepaymentPeriod,
                loanProduct.DaysInYear, loanAccount!.RepaymentCommencementDate.DateTime);

            List<AmortizationSchedule> loanSchedules = loan.GetAmortizationTable(loan.InterestCalculationMethod);

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

            var _disbursement = new CreateLoanDisbursementCommand()
            {
                TransactionType = TransactionType.LOAN_DISBURSEMENT_TOPUP,
                Amount = entity.TopupAmount,
                LoanAccountId = newLoanAccount.Id,
                DisbursementDate = DateTimeOffset.Now,
                DisbursementMode = entity.DestinationType == TopupFundingSourceType.SPECIAL_DEPOSIT_ACCOUNT ?
                    LoanDisbursementMode.SPECIAL_DEPOSIT : LoanDisbursementMode.BANK_TRANSFER,
                CustomerBankAccountId = entity.CustomerBankAccountId,
                SpecialDepositAccountId = entity.SpecialDepositAccountId,
                DisbursementAccountId = oldLoanAccount!.LoanApplication!.LoanProduct!.DisbursementAccountId,
                CreatedByUserId = entity.CreatedByUserId
            };

            var rspTxn = await _mediator.Send(_disbursement, cancellationToken);
            if (rspTxn != null && !rspTxn.ErrorFlag)
            {
                var disbursement = _dbContext.LoanDisbursements.FirstOrDefault(x => x.Id == rspTxn.Response.Id);
                disbursement!.DisbursementStatus = DisbursementStatusType.APPROVED;
                disbursement!.ApprovalDate = DateTime.UtcNow.ToLocalTime();
                _dbContext.LoanDisbursements.Update(disbursement!);
            }
        }

        entity!.Approval!.Status = request.Status;
        entity.DateUpdated = DateTime.UtcNow.ToLocalTime();
        _dbContext.LoanTopups.Update(entity!);

        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = _mapper.Map<LoanTopupViewModel>(entity);

        return rsp;
    }

    private List<LoanTopupCharge> GetLoanTopupCharges(LoanTopup entity, LoanAccount account)
    {
        var loanTopupCharges = _dbContext.LoanProductCharges
            .Include(x => x.Charge)
            .Where(x => x.ChargeType == ChargeType.LOAN_TOP_UP &&
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

        var adminTopupCharges = new List<LoanTopupCharge>();

        foreach (var adminTopupCharge in loanTopupCharges)
        {
            decimal chargeTarget = 0;
            switch (adminTopupCharge.Charge.Target)
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

            adminTopupCharges.Add(new LoanTopupCharge
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                TopupChargeId = adminTopupCharge.ChargeId,
                TotalCharge = adminTopupCharge.Charge.CalculateCharge(chargeTarget),
                LoanTopupId = entity.Id,
                ChargeType = ChargeType.LOAN_TOP_UP
            });
        }


        return adminTopupCharges;
    }
}