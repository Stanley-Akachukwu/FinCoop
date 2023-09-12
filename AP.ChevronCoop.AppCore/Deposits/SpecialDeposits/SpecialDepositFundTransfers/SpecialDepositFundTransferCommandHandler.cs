using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositFundTransfers
{
    public class SpecialDepositFundTransferCommandHandler :
      IRequestHandler<QuerySpecialDepositFundTransferCommand, CommandResult<IQueryable<SpecialDepositFundTransfer>>>,
   IRequestHandler<CreateSpecialDepositFundTransferCommand, CommandResult<SpecialDepositFundTransferViewModel>>,
   IRequestHandler<UpdateSpecialDepositFundTransferCommand, CommandResult<SpecialDepositFundTransferViewModel>>,
   IRequestHandler<DeleteSpecialDepositFundTransferCommand, CommandResult<string>>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IManageApprovalService _approvalLog;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly CoreAppSettings _options;
        public SpecialDepositFundTransferCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator, IEmailService emailService, IOptions<CoreAppSettings> options,
        ILogger<SpecialDepositFundTransferCommandHandler> _logger, IMapper _mapper, IManageApprovalService approvalLog)
        {

            _dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
            _approvalLog = approvalLog;
            _mediator = mediator;
            _emailService = emailService;
            _options = options.Value;
        }
        public Task<CommandResult<IQueryable<SpecialDepositFundTransfer>>> Handle(QuerySpecialDepositFundTransferCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<SpecialDepositFundTransfer>>();
            rsp.Response = _dbContext.SpecialDepositFundTransfers;

            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<SpecialDepositFundTransferViewModel>> Handle(CreateSpecialDepositFundTransferCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositFundTransferViewModel>();
            var entity = mapper.Map<SpecialDepositFundTransfer>(request);

            var specialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(p => p.Id == request.SpecialDepositAccountId);
            var depositProduct = _dbContext.DepositProducts.FirstOrDefault(p => p.Id == specialDepositAccount.DepositProductId);

            entity.Caption = $" {specialDepositAccount.AccountNo} - Fund Transfer.";

            if(request.DestinationAccountType== DestinationAccountType.SAVINGS_ACCOUNT)
            {
                entity.SavingsDestinationAccountId = request.SavingAccountDestinationId;
            }
            else if (request.DestinationAccountType == DestinationAccountType.FIXED_DEPOSIT_ACCOUNT)
            {
                entity.FixedDepositDestinationAccountId = request.FixedDepositDestinationAccountId;
            }
            

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == specialDepositAccount.CustomerId);

            
            var approvalRequest = new CreateApprovalModel()
            {
                Module = "Special Deposit Fund Transfer",
                Payload =  System.Text.Json.JsonSerializer.Serialize(request),
                Comment = "Create special deposit fund transfer approval initiated",
                ApprovalType = ApprovalType.SPECIAL_DEPOSIT_FUND_TRANSFER,
                Description = $"Special Deposit  Fund Transfer - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({specialDepositAccount.AccountNo})",
                CreatedBy = request.CreatedByUserId,
                EntityId = entity.Id,
                EntityType = typeof(CreateSpecialDepositFundTransferCommand),
            };


            var approval = _approvalLog.CreateApproval(approvalRequest, false, depositProduct.ApprovalWorkflowId).Result;

            entity.ApprovalId = approval.Response.Id;
            _dbContext.SpecialDepositFundTransfers.Add(entity);
            await _dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositFundTransferViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<SpecialDepositFundTransferViewModel>> Handle(UpdateSpecialDepositFundTransferCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositFundTransferViewModel>();
            var entity = await _dbContext.SpecialDepositFundTransfers.FindAsync(request.Id);

            mapper.Map(request, entity);

            try
            {

                var specialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(x => x.Id == entity.SpecialDepositAccountId);
                var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == specialDepositAccount.CustomerId);



                var transaction = new DepositTransactionCommand()
                {
                    EntityId = entity.Id,
                    EntityType = typeof(SpecialDepositFundTransfer),
                    IsApproved = true,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.SPECIAL_DEPOSIT_FUND_TRANSFER,
                    DepositAccountId = entity.SpecialDepositAccountId,
                    TransactionJournalId = null
                };

                var transactionResponse = await _mediator.Send(transaction);

                _dbContext.SpecialDepositFundTransfers.Update(entity);
                await _dbContext.SaveChangesAsync();

                //- Applicant is notified via email about transaction
                var props = new DepositAction
                {
                    ActionMessage = "Special Deposit Funds Transfer",
                    Name = $"{customer.FirstName} {customer.LastName}",

                };

                _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_ACTION, customer.PrimaryEmail, props);

            }
            catch (Exception e)
            {
                rsp.Message = e.Message;
            }



            rsp.Response = mapper.Map<SpecialDepositFundTransferViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositFundTransferCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await _dbContext.SpecialDepositFundTransfers.FindAsync(request.Id);

            _dbContext.SpecialDepositFundTransfers.Remove(entity);
            await _dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }

}


