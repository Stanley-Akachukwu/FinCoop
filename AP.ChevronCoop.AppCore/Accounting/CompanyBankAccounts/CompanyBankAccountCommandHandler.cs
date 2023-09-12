using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.CompanyBankAccounts
{
    public class CompanyBankAccountCommandHandler :
     IRequestHandler<QueryCompanyBankAccountCommand, CommandResult<IQueryable<CompanyBankAccount>>>,
    IRequestHandler<CreateCompanyBankAccountCommand, CommandResult<CompanyBankAccountViewModel>>,
    IRequestHandler<UpdateCompanyBankAccountCommand, CommandResult<CompanyBankAccountViewModel>>,
    IRequestHandler<DeleteCompanyBankAccountCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public CompanyBankAccountCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<CompanyBankAccountCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<CompanyBankAccount>>> Handle(QueryCompanyBankAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<CompanyBankAccount>>();
            rsp.Response = dbContext.CompanyBankAccounts;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<CompanyBankAccountViewModel>> Handle(CreateCompanyBankAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<CompanyBankAccountViewModel>();
            var entity = mapper.Map<CompanyBankAccount>(request);

            var currency = dbContext.Currencies.FirstOrDefault(x => x.Id == request.CurrencyId);
            var bank = dbContext.Banks.FirstOrDefault(x => x.Id == request.BankId);

            var LedgerAccount = new LedgerAccount();
            LedgerAccount.Code = NHiloHelper.GetNextKey(nameof(Entities.Accounting.LedgerAccounts.LedgerAccount)).ToString();
            LedgerAccount.Name = $"Company Bank Ledger Account- {bank.Name} ({entity.AccountNumber})";
            LedgerAccount.IsOfficeAccount = true;
            LedgerAccount.AccountType = COAType.CONTROL;
            LedgerAccount.AllowManualEntry = true;
            LedgerAccount.CurrencyId = currency.Id;
            dbContext.LedgerAccounts.Add(LedgerAccount);

            entity.LedgerAccount = LedgerAccount;


            dbContext.CompanyBankAccounts.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<CompanyBankAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<CompanyBankAccountViewModel>> Handle(UpdateCompanyBankAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<CompanyBankAccountViewModel>();
            var entity = await dbContext.CompanyBankAccounts.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.CompanyBankAccounts.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<CompanyBankAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteCompanyBankAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.CompanyBankAccounts.FindAsync(request.Id);
            entity.IsActive = false;
            entity.IsDeleted = true;
            dbContext.CompanyBankAccounts.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
