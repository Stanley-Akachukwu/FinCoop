
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
public class FixedDepositChangeInMaturityCommandHandler :
      IRequestHandler<QueryFixedDepositChangeInMaturityCommand, CommandResult<IQueryable<FixedDepositChangeInMaturity>>>,
   IRequestHandler<CreateFixedDepositChangeInMaturityCommand, CommandResult<FixedDepositChangeInMaturityViewModel>>,
   IRequestHandler<UpdateFixedDepositChangeInMaturityCommand, CommandResult<FixedDepositChangeInMaturityViewModel>>,
   IRequestHandler<DeleteFixedDepositChangeInMaturityCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IManageApprovalService _approvalService;
    private readonly IEmailService _emailService;

    public FixedDepositChangeInMaturityCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<FixedDepositChangeInMaturityCommandHandler> _logger, IMapper _mapper , IManageApprovalService manageApprovalService , IEmailService emailService)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _approvalService = manageApprovalService;
        _emailService = emailService;

    }


    public Task<CommandResult<IQueryable<FixedDepositChangeInMaturity>>> Handle(QueryFixedDepositChangeInMaturityCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<FixedDepositChangeInMaturity>>();
        rsp.Response = dbContext.FixedDepositChangeInMaturities;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<FixedDepositChangeInMaturityViewModel>> Handle(CreateFixedDepositChangeInMaturityCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositChangeInMaturityViewModel>();
        var entity = mapper.Map<FixedDepositChangeInMaturity>(request);




        if (request.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT) entity.SavingsLiquidationAccountId = request.LiquidationAccountId;

        if (request.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT) entity.SpecialDepositLiquidationAccountId = request.LiquidationAccountId;

        if (request.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT) entity.CustomerBankLiquidationAccountId = request.LiquidationAccountId;



        dbContext.FixedDepositChangeInMaturities.Add(entity);
        await dbContext.SaveChangesAsync();


        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);
        var fixedDepositAccount = await dbContext.FixedDepositAccounts.Include(x => x.DepositProduct)
                             .FirstOrDefaultAsync(c => c.Id == request.FixedDepositAccountId);


        
        var approvalRequest = new CreateApprovalModel()
        {
            Module = "DepositProducts",
            Payload = System.Text.Json.JsonSerializer.Serialize(request),
            Comment = "Create Fixed Deposit Change In Maturity approval initiated",
            ApprovalType = ApprovalType.FIXED_DEPOSIT_CHANGE_IN_MATURITY,
            Description = $"Fixed Deposit Change In Maturity  - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName}",
            CreatedBy = request.CreatedByUserId,
            EntityId = entity.Id,
            EntityType = typeof(CreateFixedDepositChangeInMaturityCommand),
        };

        var approval = await _approvalService.CreateApproval(approvalRequest, false, fixedDepositAccount.DepositProduct.ApprovalWorkflowId);
        entity.ApprovalId = approval.Response.Id;

        dbContext.FixedDepositChangeInMaturities.Update(entity);
        await dbContext.SaveChangesAsync();


        rsp.Response = mapper.Map<FixedDepositChangeInMaturityViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<FixedDepositChangeInMaturityViewModel>> Handle(UpdateFixedDepositChangeInMaturityCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositChangeInMaturityViewModel>();
        var entity = await dbContext.FixedDepositChangeInMaturities.FindAsync(request.Id);


        var  fixedDepositAccount = await dbContext.FixedDepositAccounts.
                                     FirstOrDefaultAsync(x => x.Id == request.FixedDepositAccountId);

        fixedDepositAccount.MaturityInstructionType = request.MaturityInstructionType;
        fixedDepositAccount.LiquidationAccountType = request.LiquidationAccountType;


        if (request.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT)
        {
            fixedDepositAccount.SavingsLiquidationAccountId = request.LiquidationAccountId; 
            fixedDepositAccount.SpecialDepositLiquidationAccountId = null;   
            fixedDepositAccount.CustomerBankLiquidationAccountId = null;
       
        }

        if (request.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT)
        {
            fixedDepositAccount.SpecialDepositLiquidationAccountId = request.LiquidationAccountId;
            fixedDepositAccount.SavingsLiquidationAccountId = null;
            fixedDepositAccount.CustomerBankLiquidationAccountId = null;
        }

        if (request.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
        {
            fixedDepositAccount.CustomerBankLiquidationAccountId = request.LiquidationAccountId;
            fixedDepositAccount.SavingsLiquidationAccountId = null;
            fixedDepositAccount.SpecialDepositLiquidationAccountId = null;
        }

        dbContext.FixedDepositAccounts.Update(fixedDepositAccount);
        await dbContext.SaveChangesAsync();


        var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == fixedDepositAccount.CustomerId);

        var props = new DepositAction
        {
            ActionMessage = "Fixed Deposit Change in Maturity",
            Name = $"{customer.FirstName} {customer.LastName}",

        };

        _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_ACTION, customer.PrimaryEmail, props);


        rsp.Response = mapper.Map<FixedDepositChangeInMaturityViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteFixedDepositChangeInMaturityCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.FixedDepositChangeInMaturities.FindAsync(request.Id);

        dbContext.FixedDepositChangeInMaturities.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



