using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccountApplications;

public class SavingsAccountApplicationCommandHandler :
      IRequestHandler<QuerySavingsAccountApplicationCommand, CommandResult<IQueryable<SavingsAccountApplication>>>,
   IRequestHandler<CreateSavingsAccountApplicationCommand, CommandResult<SavingsAccountApplicationViewModel>>,
   IRequestHandler<UpdateSavingsAccountApplicationCommand, CommandResult<SavingsAccountApplicationViewModel>>,
   IRequestHandler<DeleteSavingsAccountApplicationCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IManageApprovalService _approvalService;
    private readonly IEmailService _emailService;
    private readonly CoreAppSettings _options;


    public SavingsAccountApplicationCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<SavingsAccountApplicationCommandHandler> _logger, IMapper _mapper , IManageApprovalService manageApprovalService, IEmailService emailService, IOptions<CoreAppSettings> options)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _approvalService = manageApprovalService;
        _emailService = emailService;
        _options = options.Value;
    }


    public Task<CommandResult<IQueryable<SavingsAccountApplication>>> Handle(QuerySavingsAccountApplicationCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<SavingsAccountApplication>>();
        rsp.Response = dbContext.SavingsAccountApplications;
        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<SavingsAccountApplicationViewModel>> Handle(CreateSavingsAccountApplicationCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<SavingsAccountApplicationViewModel>();
        var entity = mapper.Map<SavingsAccountApplication>(request);
        entity.ApplicationNo = NHiloHelper.GetNextKey(nameof(SavingsAccountApplication)).ToString();

        var depositProduct = dbContext.DepositProducts.Where(x => x.Id == request.DepositProductId).First();
       
        
        entity.Caption = $" {depositProduct.Name} ({depositProduct.Code}) - {entity.ApplicationNo}";
        dbContext.SavingsAccountApplications.Add(entity);

        var applicant = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);

       
        var approvalRequest = new CreateApprovalModel()
        {
            Module = "DepositProducts",
            Payload = System.Text.Json.JsonSerializer.Serialize(request),
            Comment = "Create Savings Deposit account application approval initiated",
            ApprovalType = ApprovalType.SAVING_DEPOSIT_APPLICATION,
            Description = $"Savings Account Application - {applicant?.FirstName} {applicant?.MiddleName} {applicant?.LastName} ({entity.ApplicationNo})",
            CreatedBy = request.CreatedByUserId,
            EntityId = entity.Id,
            EntityType =typeof(CreateSavingsAccountApplicationCommand),
        };
        
        var approval = await _approvalService.CreateApproval(approvalRequest, false, depositProduct.ApprovalWorkflowId);
        entity.ApprovalId = approval.Response.Id;

        dbContext.SavingsAccountApplications.Update(entity);
        await dbContext.SaveChangesAsync();


        rsp.Response = mapper.Map<SavingsAccountApplicationViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<SavingsAccountApplicationViewModel>> Handle(UpdateSavingsAccountApplicationCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsAccountApplicationViewModel>();
        var entity = await dbContext.SavingsAccountApplications.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.SavingsAccountApplications.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<SavingsAccountApplicationViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteSavingsAccountApplicationCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.SavingsAccountApplications.FindAsync(request.Id);

        dbContext.SavingsAccountApplications.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



