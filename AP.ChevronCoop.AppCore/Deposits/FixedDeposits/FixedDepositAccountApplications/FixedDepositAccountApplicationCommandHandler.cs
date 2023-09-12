using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using Newtonsoft.Json;
using AP.ChevronCoop.AppCore.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositAccountApplications;
public class FixedDepositAccountApplicationCommandHandler :
      IRequestHandler<QueryFixedDepositAccountApplicationCommand, CommandResult<IQueryable<FixedDepositAccountApplication>>>,
   IRequestHandler<CreateFixedDepositAccountApplicationCommand, CommandResult<FixedDepositAccountApplicationViewModel>>,
   IRequestHandler<UpdateFixedDepositAccountApplicationCommand, CommandResult<FixedDepositAccountApplicationViewModel>>,
   IRequestHandler<DeleteFixedDepositAccountApplicationCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IManageApprovalService _manageApprovalService;
    private readonly IEmailService _emailService;
    private readonly CoreAppSettings _options;
    public FixedDepositAccountApplicationCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<FixedDepositAccountApplicationCommandHandler> _logger, IMapper _mapper, IManageApprovalService manageApprovalService, IEmailService emailService, IOptions<CoreAppSettings> options)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _manageApprovalService = manageApprovalService;
        _emailService = emailService;
        _options = options.Value;
    }



    public Task<CommandResult<IQueryable<FixedDepositAccountApplication>>> Handle(QueryFixedDepositAccountApplicationCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<FixedDepositAccountApplication>>();
        rsp.Response = dbContext.FixedDepositAccountApplications;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<FixedDepositAccountApplicationViewModel>> Handle(CreateFixedDepositAccountApplicationCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositAccountApplicationViewModel>();
        var entity = mapper.Map<FixedDepositAccountApplication>(request);


        entity.ApplicationNo = NHiloHelper.GetNextKey(nameof(FixedDepositAccountApplication)).ToString();


        var product = dbContext.DepositProducts.Where(p => p.Id == request.DepositProductId).FirstOrDefault();

        entity.TenureUnit = product!.Tenure;
        entity.TenureValue = product.TenureValue;

        //var interestRate = product.InterestRanges.Where(x => request.Amount >= x.LowerLimit && request.Amount <= x.UpperLimit).FirstOrDefault().InterestRate;

        entity.InterestRate = entity.InterestRate;

        if (request.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT) entity.SavingsLiquidationAccountId = request.LiquidationAccountId;

        if (request.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT) entity.SpecialDepositLiquidationAccountId = request.LiquidationAccountId;

        if (request.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT) entity.CustomerBankLiquidationAccountId = request.LiquidationAccountId;


        if (request.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
        {
            var document = mapper.Map<CustomerPaymentDocument>(request);
            document.DocumentType = MemberPaymentUploadType.FIXED_DEPOSIT_PAYMENT;

            dbContext.CustomerPaymentDocuments.Add(document);
            entity.PaymentDocumentId = document.Id;
        }


        if (request.ModeOfPayment == DepositFundingSourceType.SPECIAL_DEPOSIT)
            entity.SpecialDepositFundingSourceAccountId = request.ModeOfPaymentAccountId;

        entity.Caption = $" {product.Name} ({product.Code}) - {entity.ApplicationNo}";
        dbContext.FixedDepositAccountApplications.Add(entity);
        await dbContext.SaveChangesAsync();


        var applicant = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);


        var approvalRequest = new CreateApprovalModel()
        {
            Module = "DepositProducts",
            Payload = System.Text.Json.JsonSerializer.Serialize(request),
            Comment = "Create Fixed Deposit account application approval initiated",
            ApprovalType = ApprovalType.FIXED_DEPOSIT_APPLICATION,
            Description = $"Fixed Deposit Product Application - {applicant?.FirstName} {applicant?.MiddleName} {applicant?.LastName} ({entity.ApplicationNo})",
            EntityId = entity.Id,
            EntityType = typeof(CreateFixedDepositAccountApplicationCommand),
            CreatedBy = request.CreatedByUserId
        };


        var approval = await _manageApprovalService.CreateApproval(approvalRequest, false, product.ApprovalWorkflowId);
        entity.ApprovalId = approval.Response.Id;

        dbContext.FixedDepositAccountApplications.Update(entity);
        await dbContext.SaveChangesAsync();


        //var props = new GeneralEmailDto
        //{
        //    Link = $"{_options.WebBaseUrl}",
        //    Name = $"{applicant.FirstName} {applicant.LastName}"
        //};

        //_ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_APPLICATION, applicant.PrimaryEmail, props);


        rsp.Response = mapper.Map<FixedDepositAccountApplicationViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<FixedDepositAccountApplicationViewModel>> Handle(UpdateFixedDepositAccountApplicationCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositAccountApplicationViewModel>();
        var entity = await dbContext.FixedDepositAccountApplications.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.FixedDepositAccountApplications.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<FixedDepositAccountApplicationViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteFixedDepositAccountApplicationCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.FixedDepositAccountApplications.FindAsync(request.Id);

        dbContext.FixedDepositAccountApplications.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



