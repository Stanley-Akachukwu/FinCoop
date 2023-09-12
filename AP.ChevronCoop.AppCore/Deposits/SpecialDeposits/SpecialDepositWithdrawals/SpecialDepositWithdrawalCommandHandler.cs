using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using Microsoft.AspNetCore.Http;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public class SpecialDepositWithdrawalCommandHandler :
	  IRequestHandler<QuerySpecialDepositWithdrawalCommand, CommandResult<IQueryable<SpecialDepositWithdrawal>>>,
   IRequestHandler<CreateSpecialDepositWithdrawalCommand, CommandResult<SpecialDepositWithdrawalViewModel>>,
   IRequestHandler<UpdateSpecialDepositWithdrawalCommand, CommandResult<SpecialDepositWithdrawalViewModel>>,
   IRequestHandler<DeleteSpecialDepositWithdrawalCommand, CommandResult<string>>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IManageApprovalService _approvalLog;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly CoreAppSettings _options;
        public SpecialDepositWithdrawalCommandHandler(ChevronCoopDbContext appDbContext, IEmailService emailService, IOptions<CoreAppSettings> options,
        ILogger<SpecialDepositWithdrawalCommandHandler> _logger, IMapper _mapper, IManageApprovalService approvalLog, IMediator mediator)
        {
            _dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
            _approvalLog = approvalLog;
            _mediator= mediator;
            _emailService = emailService;
            _options = options.Value;
        }


		public Task<CommandResult<IQueryable<SpecialDepositWithdrawal>>> Handle(QuerySpecialDepositWithdrawalCommand request, CancellationToken cancellationToken)
        {
			var rsp = new CommandResult<IQueryable<SpecialDepositWithdrawal>>();
            rsp.Response = _dbContext.SpecialDepositWithdrawals;
            return Task.FromResult(rsp);
        }
        public async Task<CommandResult<SpecialDepositWithdrawalViewModel>> Handle(CreateSpecialDepositWithdrawalCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<SpecialDepositWithdrawalViewModel>();
            var entity = mapper.Map<SpecialDepositWithdrawal>(request);
            var customerBankAccount = new CustomerBankAccount();
            var specialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(p => p.Id == request.SpecialDepositSourceAccountId);
            var depositProduct = _dbContext.DepositProducts.FirstOrDefault(p => p.Id == specialDepositAccount.DepositProductId);

            entity.Caption = $" {specialDepositAccount.AccountNo} - Fund Withdrawal.";

            if (request.WithdrawalDestinationType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
            {
                 customerBankAccount = _dbContext.CustomerBankAccounts.FirstOrDefault(c => c.Id == entity.CustomerDestinationBankAccountId);
                if (customerBankAccount == null)
                {
                    rsp.Message = "Invalid Customer Destination Bank Account";
                    return rsp;
                }
                entity.CustomerDestinationBankAccount = customerBankAccount;
                entity.CustomerDestinationBankAccountId = entity.CustomerDestinationBankAccountId;
            }



            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == specialDepositAccount.CustomerId, cancellationToken: cancellationToken);
            var approvalRequest = new CreateApprovalModel()
            {
                Module = "Special Deposit Fund Withdrawal",
                Payload =  System.Text.Json.JsonSerializer.Serialize(request),
                Comment = "Create special deposit fund withdrawal approval initiated",
                ApprovalType = ApprovalType.SPECIAL_DEPOSIT_WITHDRAWAL,
                Description = $"Special Deposit  Fund Withdrawal - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({specialDepositAccount.AccountNo})",
                CreatedBy = request.CreatedByUserId,
                EntityId = entity.Id,
                EntityType = typeof(CreateSpecialDepositWithdrawalCommand),
            };
            var approval = _approvalLog.CreateApproval(approvalRequest, false, depositProduct.ApprovalWorkflowId).Result;


            entity.ApprovalId = approval.Response.Id;
            _dbContext.SpecialDepositWithdrawals.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = mapper.Map<SpecialDepositWithdrawalViewModel>(entity);

            return rsp;
        }
        public async Task<CommandResult<SpecialDepositWithdrawalViewModel>> Handle(UpdateSpecialDepositWithdrawalCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositWithdrawalViewModel>();
            var entity = await _dbContext.SpecialDepositWithdrawals.FindAsync(request.Id);

            mapper.Map(request, entity);

            try
            {
                var specialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(x => x.Id == entity.SpecialDepositSourceAccountId);
                var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == specialDepositAccount.CustomerId);
                
                _dbContext.SpecialDepositWithdrawals.Update(entity);
                await _dbContext.SaveChangesAsync();
                
                var transaction = new DepositTransactionCommand()
                {
                    EntityId = entity.Id,
                    EntityType = typeof(SpecialDepositWithdrawal),
                    IsApproved = true,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.SPECIAL_DEPOSIT_WITHDRAWAL,
                    DepositAccountId = request.SpecialDepositSourceAccountId,
                    TransactionJournalId = null
                };

                var transactionResponse = await _mediator.Send(transaction);

                var props = new DepositAction
                {
                    ActionMessage = "Special Deposit Withdrawal",
                    Name = $"{customer.FirstName} {customer.LastName}",
                };

                _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_ACTION, customer.PrimaryEmail, props);
            }
            catch (Exception e)
            {
                rsp.Message = e.Message;
                rsp.StatusCode = StatusCodes.Status500InternalServerError;
                return rsp;
            }

            rsp.Response = mapper.Map<SpecialDepositWithdrawalViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositWithdrawalCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await _dbContext.SpecialDepositWithdrawals.FindAsync(request.Id);

            _dbContext.SpecialDepositWithdrawals.Remove(entity);
            await _dbContext.SaveChangesAsync();
           
            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }
	
    }


