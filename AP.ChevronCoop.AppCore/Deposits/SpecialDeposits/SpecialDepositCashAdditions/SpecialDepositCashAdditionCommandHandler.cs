using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public class SpecialDepositCashAdditionCommandHandler :
      IRequestHandler<QuerySpecialDepositCashAdditionCommand, CommandResult<IQueryable<SpecialDepositCashAddition>>>,
   IRequestHandler<CreateSpecialDepositCashAdditionCommand, CommandResult<SpecialDepositCashAdditionViewModel>>,
   IRequestHandler<UpdateSpecialDepositCashAdditionCommand, CommandResult<SpecialDepositCashAdditionViewModel>>,
   IRequestHandler<DeleteSpecialDepositCashAdditionCommand, CommandResult<string>>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IManageApprovalService _approval;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly CoreAppSettings _options;

        public SpecialDepositCashAdditionCommandHandler(ChevronCoopDbContext appDbContext, IEmailService emailService, IOptions<CoreAppSettings> options,
        ILogger<SpecialDepositCashAdditionCommandHandler> _logger, IMapper _mapper, IManageApprovalService approval, IMediator mediator)
        {
            _dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
            _approval = approval;
            _mediator = mediator;
            _emailService = emailService;
            _options = options.Value;

        }

        public Task<CommandResult<IQueryable<SpecialDepositCashAddition>>> Handle(QuerySpecialDepositCashAdditionCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<SpecialDepositCashAddition>>();
            rsp.Response = _dbContext.SpecialDepositCashAdditions;
            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<SpecialDepositCashAdditionViewModel>> Handle(CreateSpecialDepositCashAdditionCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<SpecialDepositCashAdditionViewModel>();
            var entity = mapper.Map<SpecialDepositCashAddition>(request);

            var specialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(p => p.Id == request.SpecialDepositAccountId);
            var depositProduct = _dbContext.DepositProducts.FirstOrDefault(p => p.Id == specialDepositAccount.DepositProductId);

            if (request.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
            {
                var customerPaymentDocument = new CustomerPaymentDocument();

                customerPaymentDocument = new CustomerPaymentDocument
                {
                    CustomerId = specialDepositAccount.CustomerId,
                    Document = request.Document,
                    FileName = request.FileName,
                    FileSize = request.FileSize,
                    MimeType = request.MimeType,
                };
                _dbContext.CustomerPaymentDocuments.Add(customerPaymentDocument);
                await _dbContext.SaveChangesAsync();
                entity.CustomerPaymentDocumentId = customerPaymentDocument.Id;
            }

            entity.Caption = $" {specialDepositAccount.AccountNo} - Cash Addition.";


            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == specialDepositAccount.CustomerId);

           
            var approvalRequest = new CreateApprovalModel()
            {
                Module = "Special Deposit Cash Addition",
                Payload = System.Text.Json.JsonSerializer.Serialize(request),
                Comment = "Create special deposit cash addition approval initiated",
                ApprovalType = ApprovalType.SPECIAL_DEPOSIT_CASH_ADDITION,
                Description = $"Special Deposit Cash Addition - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({specialDepositAccount.AccountNo})",
                CreatedBy = request.CreatedByUserId,
                EntityId = entity.Id,
                EntityType = typeof(CreateSpecialDepositCashAdditionCommand),
            };

            var approval = _approval.CreateApproval(approvalRequest, false, depositProduct.ApprovalWorkflowId).Result;


            entity.ApprovalId = approval.Response.Id;
            _dbContext.SpecialDepositCashAdditions.Add(entity);
            await _dbContext.SaveChangesAsync();
            rsp.Response = mapper.Map<SpecialDepositCashAdditionViewModel>(entity);
            return rsp;
        }
        public async Task<CommandResult<SpecialDepositCashAdditionViewModel>> Handle(UpdateSpecialDepositCashAdditionCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositCashAdditionViewModel>();
            var entity = await _dbContext.SpecialDepositCashAdditions.FindAsync(request.Id);
            mapper.Map(request, entity);
            var response = new CommandResult<bool>();
            try
            {

                var specialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(x => x.Id == entity.SpecialDepositAccountId);
                var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == specialDepositAccount.CustomerId);
              
                _dbContext.SpecialDepositCashAdditions.Update(entity);
                await _dbContext.SaveChangesAsync();


                var cashAdditionRequest = await _dbContext.SpecialDepositCashAdditions.FirstOrDefaultAsync(x => x.Id == request.Id);

                var transaction = new DepositTransactionCommand()
                {
                    EntityId = entity.Id,
                    EntityType = typeof(SpecialDepositCashAddition),
                    IsApproved = true,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.SPECIAL_DEPOSIT_CASH_ADDITION,
                    DepositAccountId = entity.SpecialDepositAccountId,
                    TransactionJournalId = null
                };
                var transactionResponse = await _mediator.Send(transaction);

                //- Applicant is notified via email about transaction
                var props = new DepositAction
                {
                    ActionMessage = "Special Deposit Cash Addition",
                    Name = $"{customer.FirstName} {customer.LastName}",

                };

                _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_ACTION, customer.PrimaryEmail, props);
                response.Response = true;
            }
            catch (Exception e)
            {
                response.Response = false;
                response.Message = e.Message;
            }
            rsp.Response = mapper.Map<SpecialDepositCashAdditionViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositCashAdditionCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await _dbContext.SpecialDepositCashAdditions.FindAsync(request.Id);

            _dbContext.SpecialDepositCashAdditions.Remove(entity);
            await _dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }

}


