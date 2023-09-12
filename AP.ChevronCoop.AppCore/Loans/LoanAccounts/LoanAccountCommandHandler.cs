using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.Loans.LoanAccounts;

public class LoanAccountCommandHandler :
    IRequestHandler<QueryLoanAccountCommand, CommandResult<IQueryable<LoanAccount>>>,
    IRequestHandler<GetLoanAccountCommand, CommandResult<GetLoanAccountViewModel>>,
    IRequestHandler<CreateLoanAccountCommand, CommandResult<LoanAccountViewModel>>,
    IRequestHandler<UpdateLoanAccountCommand, CommandResult<LoanAccountViewModel>>,
    IRequestHandler<DeleteLoanAccountCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly ILogger<LoanAccountCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly CoreAppSettings _options;


    public LoanAccountCommandHandler(ChevronCoopDbContext dbContext, IOptions<CoreAppSettings> options,
        IEmailService emailService,
        ILogger<LoanAccountCommandHandler> logger, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _options = options.Value;
        _emailService = emailService;
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }


    public async Task<CommandResult<LoanAccountViewModel>> Handle(CreateLoanAccountCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanAccountViewModel>();

        var loanApplication = await _dbContext.LoanApplications
            .Include(x => x.LoanProduct)
            .FirstOrDefaultAsync(x => x.Id == request.LoanApplicationId, cancellationToken);

        var currency = loanApplication!.LoanProduct.DefaultCurrencyId;

        var entity = _mapper.Map<LoanAccount>(loanApplication);
        var accountNo = NHiloHelper.GetNextKey(nameof(LoanAccount)).ToString();

        entity.AccountNo = accountNo;
        entity.LoanApplicationId = loanApplication.Id;
        entity.DestinationAccountId = loanApplication.CustomerDisbursementAccountId;
        entity.SpecialDepositAccountId = loanApplication.SpecialDepositId;
        entity.IsClosed = false;
        entity.LoanCreationType = LoanCreationType.APPLICATION;

        //??!! entity CANNOT be a parent to itself
        //entity.ParentAccountId = entity.Id;
        //entity.RootParentAccountId = entity.Id;

        // Create Ledger Accounts
        var PrincipalBalanceAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Principal Balance Account ({accountNo})",
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            CurrencyId = currency,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(PrincipalBalanceAccount);
        entity.PrincipalBalanceAccount = PrincipalBalanceAccount;

        var PrincipalLossAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Principal Loss Account ({accountNo})",
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            CurrencyId = currency,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(PrincipalLossAccount);
        entity.PrincipalLossAccount = PrincipalLossAccount;

        var EarnedInterestAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Earned Interest Account ({accountNo})",
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            CurrencyId = currency
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(EarnedInterestAccount);
        entity.EarnedInterestAccount = EarnedInterestAccount;

        var InterestBalanceAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Interest Balance Account ({accountNo})",
            CurrencyId = currency,
            AllowManualEntry = true,
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(InterestBalanceAccount);
        entity.InterestBalanceAccount = InterestBalanceAccount;

        var UnearnedInterestAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Unearned Interest Account ({accountNo})",
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            CurrencyId = currency
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(UnearnedInterestAccount);
        entity.UnearnedInterestAccount = UnearnedInterestAccount;

        var InterestLossAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Interest Loss Account ({accountNo})",
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            CurrencyId = currency
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(InterestLossAccount);
        entity.InterestLossAccount = InterestLossAccount;

        var InterestWaivedAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Interest Waived Account ({accountNo})",
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            CurrencyId = currency
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(InterestWaivedAccount);
        entity.InterestWaivedAccount = InterestWaivedAccount;

        var ChargesWaivedAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Charges Waived Account ({accountNo})",
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            CurrencyId = currency
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(ChargesWaivedAccount);
        entity.ChargesWaivedAccount = ChargesWaivedAccount;

        var chargesAccruedAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Charges Accrued Account ({accountNo})",
            CurrencyId = currency,
            AllowManualEntry = true,
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(chargesAccruedAccount);
        entity.ChargesAccruedAccount = chargesAccruedAccount;

        var chargesIncomeAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Charges Income Account ({accountNo})",
            CurrencyId = currency,
            AllowManualEntry = true,
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(chargesIncomeAccount);
        entity.ChargesIncomeAccount = chargesIncomeAccount;

        var InterestEarnedAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Interest Earned Account ({accountNo})",
            CurrencyId = currency,
            AllowManualEntry = true,
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(InterestEarnedAccount);
        entity.EarnedInterestAccount = InterestEarnedAccount;

        var InterestPayoutAccount = new LedgerAccount
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Loan Account - Interest Payout Account ({accountNo})",
            IsOfficeAccount = true,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            CurrencyId = currency
           ,
            Description = "Loan Account"
        };
        _dbContext.LedgerAccounts.Add(InterestPayoutAccount);
        entity.InterestPayoutAccount = InterestPayoutAccount;

        _dbContext.LoanAccounts.Add(entity);

        // Update loan application
        loanApplication.Status = LoanApplicationStatus.APPROVED;
        loanApplication.AccountNo = accountNo;
        _dbContext.LoanApplications.Update(loanApplication);
        await _dbContext.SaveChangesAsync(cancellationToken);


        var _disbursement = new CreateLoanDisbursementCommand
        {
            Amount = loanApplication.Principal,
            LoanAccountId = entity.Id,
            DisbursementDate = DateTimeOffset.Now,
            DisbursementMode = loanApplication.UseSpecialDeposit
                ? LoanDisbursementMode.SPECIAL_DEPOSIT
                : LoanDisbursementMode.BANK_TRANSFER,
            CustomerBankAccountId = loanApplication.CustomerDisbursementAccountId,
            SpecialDepositAccountId = loanApplication.SpecialDepositId,
            DisbursementAccountId = loanApplication.LoanProduct.DisbursementAccountId
        };

        var rspTxn = await _mediator.Send(_disbursement, cancellationToken);

        if (rspTxn != null && !rspTxn.ErrorFlag)
        {
            var disbursement = _dbContext.LoanDisbursements.FirstOrDefault(x => x.Id == rspTxn.Response.Id);
            disbursement!.DisbursementStatus = DisbursementStatusType.APPROVED;
            disbursement!.ApprovalDate = DateTime.UtcNow.ToLocalTime();
            _dbContext.LoanDisbursements.Update(disbursement!);
        }

        var applicant =
            await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == loanApplication.CustomerId, cancellationToken);
        var props = new GeneralEmailDto
        {
            Link = $"{_options.WebBaseUrl}",
            Name = $"{applicant!.FirstName} {applicant.LastName}"
        };

        _ = _emailService.SendEmailAsync(EmailTemplateType.LOAN_APPROVAL, applicant.PrimaryEmail, props);


        // Create loan repayment schedules
        var loan = new LoanHelper(loanApplication.Principal, loanApplication.LoanProduct.InterestRate,
            loanApplication.LoanProduct.InterestMethod, loanApplication.LoanProduct.InterestCalculationMethod,
            loanApplication.LoanProduct.TenureUnit, loanApplication.TenureValue,
            loanApplication.LoanProduct.RepaymentPeriod, loanApplication.LoanProduct.DaysInYear,
            loanApplication.RepaymentCommencementDate.UtcDateTime);

        var loanSchedules = loan.GetAmortizationTable(loan.InterestCalculationMethod);

        var batchRef = NHiloHelper.GetNextKey(nameof(LoanRepaymentSchedule)).ToString();

        var loanRepaymentSchedules = _mapper.Map<List<LoanRepaymentSchedule>>(loanSchedules);
        loanRepaymentSchedules.ForEach(x =>
        {
            x.BatchRefNo = batchRef;
            x.LoanAccountId = entity.Id;
            x.IsPrincipalAllocated = true;
            x.IsInterestAllocated = true;
        });

        _dbContext.LoanRepaymentSchedules.AddRange(loanRepaymentSchedules);

        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = _mapper.Map<LoanAccountViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanAccountCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await _dbContext.LoanAccounts.FindAsync(request.Id);
        entity.IsDeleted = true;
        _dbContext.LoanAccounts.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = "Data successfully deleted";

        return rsp;
    }

    public async Task<CommandResult<GetLoanAccountViewModel>> Handle(GetLoanAccountCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<GetLoanAccountViewModel>();
        var entity = await _dbContext.LoanAccountMasterView.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var response = _mapper.Map<GetLoanAccountViewModel>(entity);

        // Get repayment schedule
        var repaymentSchedule = await _dbContext.LoanRepaymentSchedules
            .Where(x => x.LoanAccountId == entity!.Id && !x.IsPaid)
            .OrderBy(x => x.DueDate)
            .FirstOrDefaultAsync(cancellationToken);

        response.PeriodPayment = 0;
        if (repaymentSchedule is not null) response.PeriodPayment = repaymentSchedule.PeriodPayment;

        if (entity!.PrincipalBalanceAccountId_LedgerBalance is 0)
        {
            response.AmountPaid = 0;
            rsp.Response = response;
            return rsp;
        }

        response.AmountPaid = entity!.Principal - (entity.EarnedInterestAccountId_LedgerBalance + entity.PrincipalBalanceAccountId_LedgerBalance);

        rsp.Response = response;

        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanAccount>>> Handle(QueryLoanAccountCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanAccount>>();
        rsp.Response = _dbContext.LoanAccounts;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanAccountViewModel>> Handle(UpdateLoanAccountCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanAccountViewModel>();
        var entity = await _dbContext.LoanAccounts.FindAsync(request.Id);

        _mapper.Map(request, entity);

        _dbContext.LoanAccounts.Update(entity!);
        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = _mapper.Map<LoanAccountViewModel>(entity);

        return rsp;
    }
}