
using AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccountApplications
{
    public class SpecialDepositAccountApplicationCommandHandler :
	  IRequestHandler<QuerySpecialDepositAccountApplicationCommand, CommandResult<IQueryable<SpecialDepositAccountApplication>>>,
   IRequestHandler<CreateSpecialDepositAccountApplicationCommand, CommandResult<SpecialDepositAccountApplicationViewModel>>,
   IRequestHandler<UpdateSpecialDepositAccountApplicationCommand, CommandResult<SpecialDepositAccountApplicationViewModel>>,
   IRequestHandler<DeleteSpecialDepositAccountApplicationCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IManageApprovalService _approvalLog;
        private readonly IEmailService _emailService;
        private readonly CoreAppSettings _options;
        public SpecialDepositAccountApplicationCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<SpecialDepositAccountApplicationCommandHandler> _logger, IMapper _mapper, IManageApprovalService approvalLog, IEmailService emailService, IOptions<CoreAppSettings> options)
        {
            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
            _approvalLog = approvalLog;
            _emailService = emailService;
            _options = options.Value;
        }
 

        public Task<CommandResult<IQueryable<SpecialDepositAccountApplication>>> Handle(QuerySpecialDepositAccountApplicationCommand request, CancellationToken cancellationToken)
        {
			var rsp = new CommandResult<IQueryable<SpecialDepositAccountApplication>>();
            rsp.Response = dbContext.SpecialDepositAccountApplications;
            return Task.FromResult(rsp);
        }
        public async Task<CommandResult<SpecialDepositAccountApplicationViewModel>> Handle(CreateSpecialDepositAccountApplicationCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<SpecialDepositAccountApplicationViewModel>();
            var entity = mapper.Map<SpecialDepositAccountApplication>(request);

            entity.ApplicationNo = NHiloHelper.GetNextKey(nameof(SpecialDepositAccountApplication)).ToString();
            var depositProduct = dbContext.DepositProducts.FirstOrDefault(p => p.Id == request.DepositProductId);


            if (request.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
            {
                var customerPaymentDocument = new CustomerPaymentDocument();

                customerPaymentDocument = new CustomerPaymentDocument
                {
                    CustomerId = request.CustomerId,
                    Document = request.Document,
                    FileName = request.FileName,
                    FileSize = request.FileSize,
                    MimeType = request.MimeType,
                };
                dbContext.CustomerPaymentDocuments.Add(customerPaymentDocument);
                entity.PaymentDocumentId = customerPaymentDocument.Id;
            }
            
            entity.Caption = $" {depositProduct.Name} ({depositProduct.Code}) - {entity.ApplicationNo}";


            dbContext.SpecialDepositAccountApplications.Add(entity);
            await dbContext.SaveChangesAsync();

           
            var applicant = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);

          
            var approvalRequest = new CreateApprovalModel()
            {
                Module = "DepositApplication",
                Payload = System.Text.Json.JsonSerializer.Serialize(request),
                Comment = "Create special deposit account approval initiated",
                ApprovalType = ApprovalType.SPECIAL_DEPOSIT_APPLICATION,
                Description = $"Special Deposit Account Application - {applicant?.FirstName} {applicant?.MiddleName} {applicant?.LastName} ({entity.ApplicationNo})",
                CreatedBy = request.CreatedByUserId,
                EntityId = entity.Id,
                EntityType = typeof(CreateSpecialDepositAccountApplicationCommand),
            };

            var approval =  _approvalLog.CreateApproval(approvalRequest, false, depositProduct.ApprovalWorkflowId).Result;
            entity.ApprovalId = approval.Response.Id;
            dbContext.SpecialDepositAccountApplications.Update(entity);
            await dbContext.SaveChangesAsync();

            //var props = new GeneralEmailDto
            //{
            //    Link = $"{_options.WebBaseUrl}",
            //    Name = $"{applicant.FirstName} {applicant.LastName}"
            //};

            //_ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_APPLICATION, applicant.PrimaryEmail, props);

            rsp.Response = mapper.Map<SpecialDepositAccountApplicationViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<SpecialDepositAccountApplicationViewModel>> Handle(UpdateSpecialDepositAccountApplicationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositAccountApplicationViewModel>();
            var entity = await dbContext.SpecialDepositAccountApplications.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.SpecialDepositAccountApplications.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositAccountApplicationViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositAccountApplicationCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.SpecialDepositAccountApplications.FindAsync(request.Id);

            dbContext.SpecialDepositAccountApplications.Remove(entity);
            await dbContext.SaveChangesAsync();
           
            rsp.Response = "Data successfully deleted";

            return rsp;
        }
 
    }
}



