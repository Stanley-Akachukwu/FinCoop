
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public class SpecialDepositAccountCommandHandler :
       IRequestHandler<QuerySpecialDepositAccountCommand, CommandResult<IQueryable<SpecialDepositAccount>>>,
    IRequestHandler<CreateSpecialDepositAccountCommand, CommandResult<SpecialDepositAccountViewModel>>,
    IRequestHandler<UpdateSpecialDepositAccountCommand, CommandResult<SpecialDepositAccountViewModel>>,
    IRequestHandler<DeleteSpecialDepositAccountCommand, CommandResult<string>>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger<SpecialDepositAccountCommandHandler> logger;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;
        private readonly CoreAppSettings _options;
        public SpecialDepositAccountCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<SpecialDepositAccountCommandHandler> _logger, IMapper mapper, IEmailService emailService, IOptions<CoreAppSettings> options, IMediator mediator)
        {
            _dbContext = appDbContext;
            logger = _logger;
            _mapper = mapper;
            _emailService = emailService;
            _mediator = mediator;
            _options = options.Value;
        }

        public Task<CommandResult<IQueryable<SpecialDepositAccount>>> Handle(QuerySpecialDepositAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<SpecialDepositAccount>>();
            rsp.Response = _dbContext.SpecialDepositAccounts;
            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<SpecialDepositAccountViewModel>> Handle(CreateSpecialDepositAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<SpecialDepositAccountViewModel>();
            var entity = _mapper.Map<SpecialDepositAccount>(request);

            var product = _dbContext.DepositProducts.FirstOrDefault(p => p.Id == request.DepositProductId);

            var now = DateTime.Now;
            DateTime lastInterestComputationDay = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            var depositAccount = new LedgerAccount();
            depositAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();//$"SDPA-{accountNo}";
            depositAccount.Name = $"SD - Funds Deposit GL Account ({entity.AccountNo})";
            depositAccount.Description = "Special Deposit Account";
            depositAccount.IsOfficeAccount = true;
            depositAccount.AccountType = COAType.CONTROL;
            depositAccount.AllowManualEntry = true;
            depositAccount.CurrencyId = product?.DefaultCurrencyId;
            _dbContext.LedgerAccounts.Add(depositAccount);
            entity.DepositAccount = depositAccount;

            var chargesAccruedAccount = new LedgerAccount();
            chargesAccruedAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();//$"SDPCAA-{accountNo}";
            chargesAccruedAccount.Name = $"SD - Charges Accrual GL Account ({entity.AccountNo})";
            chargesAccruedAccount.Description = "Special Deposit Account";
            chargesAccruedAccount.IsOfficeAccount = true;
            chargesAccruedAccount.AccountType = COAType.CONTROL;
            chargesAccruedAccount.AllowManualEntry = true;
            chargesAccruedAccount.CurrencyId = product.DefaultCurrencyId;
            _dbContext.LedgerAccounts.Add(chargesAccruedAccount);
            entity.ChargesAccruedAccount = chargesAccruedAccount;



            var chargesIncomeAccount = new LedgerAccount();
            chargesIncomeAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();//$"SDPCIA-{accountNo}";
            chargesIncomeAccount.Name = $"SD - Charge Income GL Account ({entity.AccountNo})";
            chargesIncomeAccount.Description = "Special Deposit Account";
            chargesIncomeAccount.IsOfficeAccount = true;
            chargesIncomeAccount.AccountType = COAType.CONTROL;
            chargesIncomeAccount.AllowManualEntry = true;
            chargesIncomeAccount.CurrencyId = product.DefaultCurrencyId;
            _dbContext.LedgerAccounts.Add(chargesIncomeAccount);
            entity.ChargesIncomeAccount = chargesIncomeAccount;


            var ChargesWaivedAccount = new LedgerAccount();
            ChargesWaivedAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
            ChargesWaivedAccount.Name = $"SD - Charges Waived GL Account ({entity.AccountNo})";
            ChargesWaivedAccount.Description = "Special Deposit Account";
            ChargesWaivedAccount.IsOfficeAccount = true;
            ChargesWaivedAccount.AccountType = COAType.CONTROL;
            ChargesWaivedAccount.AllowManualEntry = true;
            ChargesWaivedAccount.CurrencyId = product.DefaultCurrencyId;
            _dbContext.LedgerAccounts.Add(ChargesWaivedAccount);
            entity.ChargesWaivedAccount = ChargesWaivedAccount;


            var interestEarnedAccount = new LedgerAccount();
            interestEarnedAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(); //$"SDPIEA-{accountNo}";
            interestEarnedAccount.Name = $"SD - Interest Earned GL Account ({entity.AccountNo})";
            interestEarnedAccount.Description = "Special Deposit Account";
            interestEarnedAccount.IsOfficeAccount = true;
            interestEarnedAccount.AccountType = COAType.CONTROL;
            interestEarnedAccount.AllowManualEntry = true;
            interestEarnedAccount.CurrencyId = product.DefaultCurrencyId;
            _dbContext.LedgerAccounts.Add(interestEarnedAccount);
            entity.InterestEarnedAccount = interestEarnedAccount;


            var interestPayoutAccount = new LedgerAccount();
            interestPayoutAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();//$"SDPIPA-{accountNo}";
            interestPayoutAccount.Name = $"SD - Interest Payout GL Account ({entity.AccountNo})";
            interestPayoutAccount.Description = "Special Deposit Account";
            interestPayoutAccount.IsOfficeAccount = true;
            interestPayoutAccount.AccountType = COAType.CONTROL;
            interestPayoutAccount.AllowManualEntry = true;
            interestPayoutAccount.CurrencyId = product.DefaultCurrencyId;
            _dbContext.LedgerAccounts.Add(interestPayoutAccount);
            entity.InterestPayoutAccount = interestPayoutAccount;

            entity.LastInterestComputationDate = lastInterestComputationDay;
            entity.Caption = $"{product.Name} ({product.Code}) - {entity.AccountNo}";

            _dbContext.SpecialDepositAccounts.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var application = await _dbContext.SpecialDepositAccountApplications.FirstOrDefaultAsync(x => x.Id == request.ApplicationId);

            if (application.ModeOfPayment != DepositFundingSourceType.PAYROLL)
            {

                var transaction = new DepositTransactionCommand()
                {
                    EntityId = entity.Id,
                    EntityType = typeof(SpecialDepositAccount),
                    IsApproved = true,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.SPECIAL_DEPOSIT_APPLICATION_FUNDING,
                    DepositAccountId = entity.Id,
                    TransactionJournalId = null
                };

                var transactionResponse = await _mediator.Send(transaction, cancellationToken);
            }



            var applicant = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken: cancellationToken);
            var props = new DepositApplicationApproval
            {
                DepositName = "Special",
                Name = $"{applicant.FirstName} {applicant.LastName}",

            };

            _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_APPLICATION_APPROVAL, applicant.PrimaryEmail, props);

            rsp.Response = _mapper.Map<SpecialDepositAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<SpecialDepositAccountViewModel>> Handle(UpdateSpecialDepositAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositAccountViewModel>();
            var entity = await _dbContext.SpecialDepositAccounts.FindAsync(request.Id);

            _mapper.Map(request, entity);

            _dbContext.SpecialDepositAccounts.Update(entity);
            await _dbContext.SaveChangesAsync();

            rsp.Response = _mapper.Map<SpecialDepositAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<SpecialDepositAccountViewModel>> Handle(UpdateSpecialDepositDefaultCreatedAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositAccountViewModel>();
            var entity = await _dbContext.SpecialDepositAccounts.FindAsync(request.SpecialDepositAccountId);

            _mapper.Map(request, entity);

            _dbContext.SpecialDepositAccounts.Update(entity);
            await _dbContext.SaveChangesAsync();

            rsp.Response = _mapper.Map<SpecialDepositAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await _dbContext.SpecialDepositAccounts.FindAsync(request.Id);

            _dbContext.SpecialDepositAccounts.Remove(entity);
            await _dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }

}


