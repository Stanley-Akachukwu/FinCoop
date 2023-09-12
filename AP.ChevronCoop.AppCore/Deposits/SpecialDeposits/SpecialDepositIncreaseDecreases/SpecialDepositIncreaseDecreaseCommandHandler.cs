using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;

public class SpecialDepositIncreaseDecreaseCommandHandler :
      IRequestHandler<QuerySpecialDepositIncreaseDecreaseCommand, CommandResult<IQueryable<SpecialDepositIncreaseDecrease>>>,
   IRequestHandler<CreateSpecialDepositIncreaseDecreaseCommand, CommandResult<SpecialDepositIncreaseDecreaseViewModel>>,
   IRequestHandler<UpdateSpecialDepositIncreaseDecreaseCommand, CommandResult<SpecialDepositIncreaseDecreaseViewModel>>,
   IRequestHandler<DeleteSpecialDepositIncreaseDecreaseCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IManageApprovalService _approvalService;
    private readonly IEmailService _emailService;

    public SpecialDepositIncreaseDecreaseCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<SpecialDepositIncreaseDecreaseCommandHandler> _logger, IMapper _mapper, IManageApprovalService manageApprovalService, IEmailService emailService)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _approvalService = manageApprovalService;
        _emailService = emailService;

    }


    public Task<CommandResult<IQueryable<SpecialDepositIncreaseDecrease>>> Handle(QuerySpecialDepositIncreaseDecreaseCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<SpecialDepositIncreaseDecrease>>();
        rsp.Response = dbContext.SpecialDepositIncreaseDecreases;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<SpecialDepositIncreaseDecreaseViewModel>> Handle(CreateSpecialDepositIncreaseDecreaseCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SpecialDepositIncreaseDecreaseViewModel>();
        var entity = mapper.Map<SpecialDepositIncreaseDecrease>(request);

        dbContext.SpecialDepositIncreaseDecreases.Add(entity);
        await dbContext.SaveChangesAsync();

        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);
        var savingAccount = await dbContext.SpecialDepositAccounts.Include(x => x.DepositProduct)
                             .FirstOrDefaultAsync(c => c.Id == request.SpecialDepositAccountId);


       
        var approvalRequest = new CreateApprovalModel()
        {
            Module = "DepositProducts",
            Payload = System.Text.Json.JsonSerializer.Serialize(request),
            Comment = "Create SpecialDeposit Deposit account application approval initiated",
            ApprovalType = ApprovalType.SPECIAL_DEPOSIT_INCREASE_DECREASE,
            Description = $"SpecialDeposit Increase Decrease - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName}",
            CreatedBy = request.CreatedByUserId,
            EntityId = entity.Id,
            EntityType = typeof(CreateSpecialDepositIncreaseDecreaseCommand),
        };

        var approval = await _approvalService.CreateApproval(approvalRequest, false, savingAccount.DepositProduct.ApprovalWorkflowId);
        entity.ApprovalId = approval.Response.Id;

        dbContext.SpecialDepositIncreaseDecreases.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<SpecialDepositIncreaseDecreaseViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<SpecialDepositIncreaseDecreaseViewModel>> Handle(UpdateSpecialDepositIncreaseDecreaseCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SpecialDepositIncreaseDecreaseViewModel>();
        var entity = await dbContext.SpecialDepositIncreaseDecreases.FindAsync(request.Id);

        var SpecialDepositAccount = await dbContext.SpecialDepositAccounts.Include(x => x.Application).
                                        FirstOrDefaultAsync(x => x.Id == request.SpecialDepositAccountId);

        SpecialDepositAccount.FundingAmount = request.Amount;

        dbContext.SpecialDepositAccounts.Update(SpecialDepositAccount);
        await dbContext.SaveChangesAsync();

        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == SpecialDepositAccount.CustomerId);

        var props = new DepositAction
        {
            ActionMessage = "SpecialDeposit Cash Addition",
            Name = $"{customer.FirstName} {customer.LastName}",

        };

        _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_ACTION, customer.PrimaryEmail, props);


        rsp.Response = mapper.Map<SpecialDepositIncreaseDecreaseViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteSpecialDepositIncreaseDecreaseCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.SpecialDepositIncreaseDecreases.FindAsync(request.Id);

        dbContext.SpecialDepositIncreaseDecreases.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



