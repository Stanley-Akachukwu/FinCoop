using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using Microsoft.Identity.Client;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccounts;


public class SavingsAccountCommandHandler :
      IRequestHandler<QuerySavingsAccountCommand, CommandResult<IQueryable<SavingsAccount>>>,
   IRequestHandler<CreateSavingsAccountCommand, CommandResult<SavingsAccountViewModel>>,
   IRequestHandler<UpdateSavingsAccountCommand, CommandResult<SavingsAccountViewModel>>,
   IRequestHandler<DeleteSavingsAccountCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IEmailService _emailService;
    private readonly CoreAppSettings _options;
    public SavingsAccountCommandHandler(ChevronCoopDbContext appDbContext, IEmailService emailService, IOptions<CoreAppSettings> options,
    ILogger<SavingsAccountCommandHandler> _logger, IMapper _mapper)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _emailService = emailService;
        _options = options.Value;
    }


    public Task<CommandResult<IQueryable<SavingsAccount>>> Handle(QuerySavingsAccountCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<SavingsAccount>>();
        rsp.Response = dbContext.SavingsAccounts;

        return Task.FromResult(rsp);
    }



    public async Task<CommandResult<SavingsAccountViewModel>> Handle(CreateSavingsAccountCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsAccountViewModel>();
        var entity = mapper.Map<SavingsAccount>(request);


        var accountNo = NHiloHelper.GetNextKey(nameof(SavingsAccount)).ToString();
        entity.AccountNo = accountNo;

        var product = dbContext.DepositProducts.Where(p => p.Id == request.DepositProductId).FirstOrDefault();

        var depositAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Savings - Deposit GL Account ({accountNo})",
            Description = "Savings Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(depositAccount);
        entity.LedgerDepositAccount = depositAccount;

        var chargesPayableAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Savings - Charges Payable GL Account ({accountNo})",
            Description = "Savings Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(chargesPayableAccount);
        entity.ChargesPayableAccount = chargesPayableAccount;


        var chargesAccruedAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Savings - Charges Accrued GL Account ({accountNo})",
            Description = "Savings Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(chargesAccruedAccount);
        entity.ChargesAccruedAccount = chargesAccruedAccount;



        var chargeIncomeAccount = new LedgerAccount()
        {
            Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
            Name = $"Savings - Charge Income GL Account ({accountNo})",
            Description = "Savings Account",
            CurrencyId = product.DefaultCurrencyId,
            AccountType = COAType.CONTROL
        };
        dbContext.LedgerAccounts.Add(chargeIncomeAccount);
        entity.ChargesIncomeAccount = chargeIncomeAccount;


        var ChargesWaivedAccount = new LedgerAccount();
        ChargesWaivedAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        ChargesWaivedAccount.Name = $"Savings - Charges Waived GL Account ({accountNo})";
        ChargesWaivedAccount.Description = "Savings Account";
        ChargesWaivedAccount.IsOfficeAccount = true;
        ChargesWaivedAccount.AccountType = COAType.CONTROL;
        ChargesWaivedAccount.AllowManualEntry = true;
        ChargesWaivedAccount.CurrencyId = product.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(ChargesWaivedAccount);
        entity.ChargesWaivedAccount = ChargesWaivedAccount;


        entity.Caption = $"{product.Name} ({product.Code}) - {entity.AccountNo}";

        dbContext.SavingsAccounts.Add(entity);
        await dbContext.SaveChangesAsync();

        var applicant = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);

        var props = new DepositApplicationApproval
        {
            DepositName = "Savings",
            Name = $"{applicant.FirstName} {applicant.LastName}",

        };

        _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_APPLICATION_APPROVAL, applicant.PrimaryEmail, props);


        rsp.Response = mapper.Map<SavingsAccountViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<SavingsAccountViewModel>> Handle(UpdateSavingsAccountCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsAccountViewModel>();
        var entity = await dbContext.SavingsAccounts.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.SavingsAccounts.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<SavingsAccountViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteSavingsAccountCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.SavingsAccounts.FindAsync(request.Id);

        dbContext.SavingsAccounts.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



