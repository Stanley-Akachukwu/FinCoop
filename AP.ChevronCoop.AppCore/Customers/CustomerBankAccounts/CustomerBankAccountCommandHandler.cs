using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Customers.CustomerBankAccounts
{
    public class CustomerBankAccountCommandHandler :
     IRequestHandler<QueryCustomerBankAccountCommand, CommandResult<IQueryable<CustomerBankAccount>>>,
    IRequestHandler<CreateCustomerBankAccountCommand, CommandResult<CustomerBankAccountViewModel>>,
    IRequestHandler<UpdateCustomerBankAccountCommand, CommandResult<CustomerBankAccountViewModel>>,
    IRequestHandler<DeleteCustomerBankAccountCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CustomerBankAccountCommandHandler> logger;
        private readonly IMapper mapper;

        public CustomerBankAccountCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<CustomerBankAccountCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<CustomerBankAccount>>> Handle(QueryCustomerBankAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<CustomerBankAccount>>();
            rsp.Response = dbContext.CustomerBankAccounts;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<CustomerBankAccountViewModel>> Handle(CreateCustomerBankAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<CustomerBankAccountViewModel>();
            var entity = mapper.Map<CustomerBankAccount>(request);
            
            var currency = dbContext.Currencies.FirstOrDefault(x => x.Code.ToLower() == "ngn");
            var customer = await dbContext.Customers.FindAsync(request.CustomerId);
            
            var ledgerAccount = new LedgerAccount();
            ledgerAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
            ledgerAccount.Name = $"Customer Bank Account ({customer.CustomerNo}-{entity.AccountNumber})";
            ledgerAccount.IsOfficeAccount = true;
            ledgerAccount.AccountType = COAType.CONTROL;
            ledgerAccount.AllowManualEntry = true;
            ledgerAccount.CurrencyId = currency.Id;
            dbContext.LedgerAccounts.Add(ledgerAccount);

            entity.LedgerAccount = ledgerAccount;

            dbContext.CustomerBankAccounts.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = mapper.Map<CustomerBankAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<CustomerBankAccountViewModel>> Handle(UpdateCustomerBankAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<CustomerBankAccountViewModel>();
            var entity = await dbContext.CustomerBankAccounts.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.CustomerBankAccounts.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = mapper.Map<CustomerBankAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteCustomerBankAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.CustomerBankAccounts.FindAsync(request.Id);
            entity.IsDeleted = true;
            dbContext.CustomerBankAccounts.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
