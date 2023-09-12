using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.BackgroundServices;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositLiquidations;

public class FixedDepositLiquidationCommandHandler :
      IRequestHandler<QueryFixedDepositLiquidationCommand, CommandResult<IQueryable<FixedDepositLiquidation>>>,
   IRequestHandler<CreateFixedDepositLiquidationCommand, CommandResult<FixedDepositLiquidationViewModel>>,
   IRequestHandler<UpdateFixedDepositLiquidationCommand, CommandResult<FixedDepositLiquidationViewModel>>,
   IRequestHandler<DeleteFixedDepositLiquidationCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly IMediator _mediator;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IManageApprovalService _approvalService;
    private readonly IEmailService _emailService;
    private readonly IFixedDepositInterestComputationService _fixedDepositInterestComputationService;

    public FixedDepositLiquidationCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator,
    ILogger<FixedDepositLiquidationCommandHandler> _logger, IMapper _mapper, IManageApprovalService manageApprovalService, IEmailService emailService , IFixedDepositInterestComputationService  fixedDepositInterestComputationService)
    {

        dbContext = appDbContext;
        _mediator = mediator;
        logger = _logger;
        mapper = _mapper;
        _approvalService = manageApprovalService;
        _emailService = emailService;
        _fixedDepositInterestComputationService = fixedDepositInterestComputationService;

    }


    public Task<CommandResult<IQueryable<FixedDepositLiquidation>>> Handle(QueryFixedDepositLiquidationCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<FixedDepositLiquidation>>();
        rsp.Response = dbContext.FixedDepositLiquidations;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<FixedDepositLiquidationViewModel>> Handle(CreateFixedDepositLiquidationCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositLiquidationViewModel>();
        var entity = mapper.Map<FixedDepositLiquidation>(request);

        entity.IsMatured = !request.IsImmediate;
       
        if (request.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT) entity.SavingsLiquidationAccountId = request.LiquidationAccountId;

        if (request.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT) entity.SpecialDepositLiquidationAccountId = request.LiquidationAccountId;

        if (request.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT) entity.CustomerBankLiquidationAccountId = request.LiquidationAccountId;




        dbContext.FixedDepositLiquidations.Add(entity);
        await dbContext.SaveChangesAsync();


        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);
        var fixedDepositAccount = await dbContext.FixedDepositAccounts.Include(x => x.DepositProduct)
                             .FirstOrDefaultAsync(c => c.Id == request.FixedDepositAccountId);


        if (request.IsImmediate)
        {

            var approvalRequest = new CreateApprovalModel()
            {
                Module = "DepositProducts",
                Payload = System.Text.Json.JsonSerializer.Serialize(request),
                Comment = "Create Fixed Deposit Liquidation approval initiated",
                ApprovalType = ApprovalType.FIXED_DEPOSIT_LIQUIDATION,
                Description = $"Fixed Deposit Liquidation  - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName}",
                CreatedBy = request.CreatedByUserId,
                EntityId = entity.Id,
                EntityType = typeof(CreateFixedDepositLiquidationCommand),
            };

            var approval = await _approvalService.CreateApproval(approvalRequest, false, fixedDepositAccount.DepositProduct.ApprovalWorkflowId);
            entity.ApprovalId = approval.Response.Id;
        }

        dbContext.FixedDepositLiquidations.Update(entity);
        await dbContext.SaveChangesAsync();

        if (entity.IsMatured)
        {

            var command = new UpdateFixedDepositLiquidationCommand
            {
                Id = entity.Id,
                FixedDepositAccountId = request.FixedDepositAccountId,
                LiquidationAccountId = request.LiquidationAccountId,
                LiquidationAccountType = request.LiquidationAccountType
                
            };

            await _mediator.Send(command);
        }



        rsp.Response = mapper.Map<FixedDepositLiquidationViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<FixedDepositLiquidationViewModel>> Handle(UpdateFixedDepositLiquidationCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositLiquidationViewModel>();
        var entity = await dbContext.FixedDepositLiquidations.
                           Include(x => x.FixedDepositAccount)
                           .ThenInclude( x => x.DepositAccount)
                           .Include(x => x.FixedDepositAccount)
                           .ThenInclude(x => x.InterestEarnedAccount)
                          .FirstOrDefaultAsync(x => x.Id == request.Id);


       
        var fixedDepositAccount = entity.FixedDepositAccount;

        entity.LiquidationDate = DateTime.Now;
        entity.MaturityDate = _fixedDepositInterestComputationService.GetMaturityDate(fixedDepositAccount);

        fixedDepositAccount.LiquidationAccountType = request.LiquidationAccountType;

        //matured  
        if (!entity.IsMatured)
        {
            fixedDepositAccount.MaturityInstructionType = MaturityInstructionType.LIQUIDATE_PRINCIPAL_AND_INTEREST;
        }


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


        if (!entity.IsMatured) await _fixedDepositInterestComputationService.ProcessFixedDepositAccountThatIsliquidatedBeforeMaturity(entity);

        
        var transaction = new DepositTransactionCommand()
        {
            EntityId = entity.Id,
            EntityType = typeof(FixedDepositLiquidation),
            IsApproved = false,
            ApprovedOn = DateTime.Now,
            TransactionAction = TransactionAction.POST,
            TransactionDate = DateTime.Now,
            TransactionType = TransactionType.FIXED_DEPOSIT_LIQUIDATION,
            DepositAccountId = entity.FixedDepositAccountId,
            TransactionJournalId = null
        };

        var transactionResponse = await _mediator.Send(transaction);

        var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == fixedDepositAccount.CustomerId);

        var props = new DepositAction
        {
            ActionMessage = "Fixed Deposit Imediate Liquidation",
            Name = $"{customer.FirstName} {customer.LastName}",

        };

        _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_ACTION, customer.PrimaryEmail, props);


        rsp.Response = mapper.Map<FixedDepositLiquidationViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteFixedDepositLiquidationCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.FixedDepositLiquidations.FindAsync(request.Id);

        dbContext.FixedDepositLiquidations.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



