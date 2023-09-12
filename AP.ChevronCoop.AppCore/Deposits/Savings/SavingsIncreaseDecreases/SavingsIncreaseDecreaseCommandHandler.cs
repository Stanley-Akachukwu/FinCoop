using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsIncreaseDecreases;

public class SavingsIncreaseDecreaseCommandHandler :
      IRequestHandler<QuerySavingsIncreaseDecreaseCommand, CommandResult<IQueryable<SavingsIncreaseDecrease>>>,
   IRequestHandler<CreateSavingsIncreaseDecreaseCommand, CommandResult<SavingsIncreaseDecreaseViewModel>>,
   IRequestHandler<UpdateSavingsIncreaseDecreaseCommand, CommandResult<SavingsIncreaseDecreaseViewModel>>,
   IRequestHandler<DeleteSavingsIncreaseDecreaseCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IManageApprovalService _approvalService;
    private readonly IEmailService _emailService;

    public SavingsIncreaseDecreaseCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<SavingsIncreaseDecreaseCommandHandler> _logger, IMapper _mapper, IManageApprovalService manageApprovalService , IEmailService emailService)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _approvalService = manageApprovalService;
        _emailService = emailService;

    }


    public Task<CommandResult<IQueryable<SavingsIncreaseDecrease>>> Handle(QuerySavingsIncreaseDecreaseCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<SavingsIncreaseDecrease>>();
        rsp.Response = dbContext.SavingsIncreaseDecreases;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<SavingsIncreaseDecreaseViewModel>> Handle(CreateSavingsIncreaseDecreaseCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsIncreaseDecreaseViewModel>();
        var entity = mapper.Map<SavingsIncreaseDecrease>(request);

        dbContext.SavingsIncreaseDecreases.Add(entity);
        await dbContext.SaveChangesAsync();

        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);
        var savingAccount = await dbContext.SavingsAccounts.Include(x => x.DepositProduct)
                             .FirstOrDefaultAsync(c => c.Id == request.SavingsAccountId);


       
        var approvalRequest = new CreateApprovalModel()
        {
            Module = "DepositProducts",
            Payload = System.Text.Json.JsonSerializer.Serialize(request),
            Comment = "Create Savings Deposit account application approval initiated",
            ApprovalType = ApprovalType.SAVINGS_INCREASE_DECREASE,
            Description = $"Savings Increase Decrease - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName}",
            CreatedBy = request.CreatedByUserId,
            EntityId = entity.Id,
            EntityType = typeof(CreateSavingsIncreaseDecreaseCommand),
        };

        var approval = await _approvalService.CreateApproval(approvalRequest, false, savingAccount.DepositProduct.ApprovalWorkflowId);
        entity.ApprovalId = approval.Response.Id;

        dbContext.SavingsIncreaseDecreases.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<SavingsIncreaseDecreaseViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<SavingsIncreaseDecreaseViewModel>> Handle(UpdateSavingsIncreaseDecreaseCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsIncreaseDecreaseViewModel>();
        var entity = await dbContext.SavingsIncreaseDecreases.FindAsync(request.Id);

        var savingsAccount = await dbContext.SavingsAccounts.Include(x => x.Application).
                                        FirstOrDefaultAsync(x => x.Id == request.SavingsAccountId);


        //if (request.ContributionChangeRequest == ContributionChangeRequest.INCREASE)
        //    savingsAccount.PayrollAmount = request.Amount;
        //else
        //    savingsAccount.PayrollAmount = request.Amount;

        savingsAccount.PayrollAmount = request.Amount;




        dbContext.SavingsAccounts.Update(savingsAccount);
        await dbContext.SaveChangesAsync();



        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == savingsAccount.CustomerId);

        var props = new DepositAction
        {
            ActionMessage = "Savings Cash Addition",
            Name = $"{customer.FirstName} {customer.LastName}",

        };

        _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_ACTION, customer.PrimaryEmail, props);


        rsp.Response = mapper.Map<SavingsIncreaseDecreaseViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteSavingsIncreaseDecreaseCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.SavingsIncreaseDecreases.FindAsync(request.Id);

        dbContext.SavingsIncreaseDecreases.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



