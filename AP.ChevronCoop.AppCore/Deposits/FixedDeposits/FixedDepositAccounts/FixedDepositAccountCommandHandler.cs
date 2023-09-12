
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;

namespace ChevronCoop.API.Controllers.Deposits.FixedDeposits.FixedDepositAccounts;
public class FixedDepositAccountCommandHandler :
      IRequestHandler<QueryFixedDepositAccountCommand, CommandResult<IQueryable<FixedDepositAccount>>>,
   IRequestHandler<CreateFixedDepositAccountCommand, CommandResult<FixedDepositAccountViewModel>>,
   IRequestHandler<UpdateFixedDepositAccountCommand, CommandResult<FixedDepositAccountViewModel>>,
   IRequestHandler<DeleteFixedDepositAccountCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IMediator _mediator;
    private readonly IEmailService _emailService;
    private readonly CoreAppSettings _options;
    public FixedDepositAccountCommandHandler(ChevronCoopDbContext appDbContext, IEmailService emailService, IOptions<CoreAppSettings> options,
    ILogger<FixedDepositAccountCommandHandler> _logger, IMapper _mapper, IMediator mediator)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _mediator = mediator;
        _emailService = emailService;
        _options = options.Value;

    }


    public Task<CommandResult<IQueryable<FixedDepositAccount>>> Handle(QueryFixedDepositAccountCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<FixedDepositAccount>>();
        rsp.Response = dbContext.FixedDepositAccounts;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<FixedDepositAccountViewModel>> Handle(CreateFixedDepositAccountCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositAccountViewModel>();
        var entity = mapper.Map<FixedDepositAccount>(request);


        var product = dbContext.DepositProducts.Where(p => p.Id == request.DepositProductId).FirstOrDefault();

        entity.TenureUnit = product!.Tenure;
        entity.TenureValue = product.TenureValue;

        var intrestRate = request.InterestRate;
        entity.InterestRate = intrestRate;


        if (request.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT)
            entity.SavingsLiquidationAccountId = request.LiquidationAccountId;


        if (request.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT)
            entity.SpecialDepositLiquidationAccountId = request.LiquidationAccountId;


        if (request.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
            entity.CustomerBankLiquidationAccountId = request.LiquidationAccountId;


        var application = await dbContext.FixedDepositAccountApplications.FirstOrDefaultAsync(x => x.Id == request.ApplicationId);

        var accountNo = NHiloHelper.GetNextKey(nameof(FixedDepositAccount)).ToString();
        entity.AccountNo = accountNo;


        var depositAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"FD Funds Deposit GL Account ({accountNo})",
            Description = "Fixed Deposit Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(depositAccount);
        entity.DepositAccount = depositAccount;

        var chargesAccruedAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"FD Charges Accrued GL Account ({accountNo})",
            Description = "Fixed Deposit Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(chargesAccruedAccount);
        entity.ChargesAccruedAccount = chargesAccruedAccount;


        var chargeIncomeAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"FD Charge Income GL Account ({accountNo})",
            Description = "Fixed Deposit Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(chargeIncomeAccount);
        entity.ChargesIncomeAccount = chargeIncomeAccount;


        var interestEarnedAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"FD Interest Earned GL Account ({accountNo})",
            Description = "Fixed Deposit Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(interestEarnedAccount);
        entity.InterestEarnedAccount = interestEarnedAccount;


        var interestPayoutAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"FD Interest Payout GL Account ({accountNo})",
            Description = "Fixed Deposit Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(interestPayoutAccount);
        entity.InterestPayoutAccount = interestPayoutAccount;


        var ChargesWaivedAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"FD Charges Waived GL Account ({accountNo})",
            Description = "Fixed Deposit Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL,
            AllowManualEntry = true,
            IsOfficeAccount = true,

        };

        dbContext.LedgerAccounts.Add(ChargesWaivedAccount);
        entity.ChargesWaivedAccount = ChargesWaivedAccount;


        entity.Caption = $" {product.Name} ({product.Code}) - {request.AccountNo}";

        dbContext.FixedDepositAccounts.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        //Specialdeposit and bank transfer

        if (application.ModeOfPayment != DepositFundingSourceType.PAYROLL && request.ParentAccountId == null)
        {

            var transaction = new DepositTransactionCommand()
            {
                EntityId = entity.Id,
                EntityType = typeof(FixedDepositAccount),
                IsApproved = false,
                ApprovedOn = DateTime.Now,
                TransactionAction = TransactionAction.POST,
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.FIXED_DEPOSIT_APPLICATION_FUNDING,
                DepositAccountId = entity.Id,
                TransactionJournalId = null
            };


            var transactionResponse = await _mediator.Send(transaction);
        }


        if (request.ParentAccountId != null)
        {


            var liquidateCommand = new CreateFixedDepositLiquidationCommand
            {
                IsImmediate = false,
                FixedDepositAccountId = entity.Id,
                CustomerId = entity.CustomerId,
                LiquidationAccountType = request.LiquidationAccountType,
                LiquidationAccountId = request.LiquidationAccountId,
                Comments = "Account Matured",
                Description = $"Account Matured",
                CreatedByUserId = entity.CreatedByUserId,
                Caption = entity.Caption,


            };

            await _mediator.Send(liquidateCommand);


        }



        if (request.ParentAccountId == null)
        {

            var applicant = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);


            var props = new DepositApplicationApproval
            {
                DepositName = "Special",
                Name = $"{applicant.FirstName} {applicant.LastName}",

            };

            _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_APPLICATION_APPROVAL, applicant.PrimaryEmail, props);
        }

        rsp.Response = mapper.Map<FixedDepositAccountViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<FixedDepositAccountViewModel>> Handle(UpdateFixedDepositAccountCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositAccountViewModel>();
        var entity = await dbContext.FixedDepositAccounts.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.FixedDepositAccounts.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<FixedDepositAccountViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteFixedDepositAccountCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.FixedDepositAccounts.FindAsync(request.Id);

        dbContext.FixedDepositAccounts.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



