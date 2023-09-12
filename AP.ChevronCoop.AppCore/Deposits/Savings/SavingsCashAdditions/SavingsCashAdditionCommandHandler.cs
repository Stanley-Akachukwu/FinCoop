using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Loans.LoanTransactions;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsCashAdditions;

public class SavingsCashAdditionCommandHandler :
      IRequestHandler<QuerySavingsCashAdditionCommand, CommandResult<IQueryable<SavingsCashAddition>>>,
   IRequestHandler<CreateSavingsCashAdditionCommand, CommandResult<SavingsCashAdditionViewModel>>,
   IRequestHandler<UpdateSavingsCashAdditionCommand, CommandResult<SavingsCashAdditionViewModel>>,
   IRequestHandler<DeleteSavingsCashAdditionCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly IMediator _mediator;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IManageApprovalService _approvalService;
    private readonly IEmailService _emailService;

    public SavingsCashAdditionCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator,
    ILogger<SavingsCashAdditionCommandHandler> _logger, IMapper _mapper, IManageApprovalService manageApprovalService, IEmailService emailService)
    {

        dbContext = appDbContext;
        _mediator = mediator;
        logger = _logger;
        mapper = _mapper;
        _approvalService = manageApprovalService;
        _emailService = emailService;

    }


    public Task<CommandResult<IQueryable<SavingsCashAddition>>> Handle(QuerySavingsCashAdditionCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<SavingsCashAddition>>();
        rsp.Response = dbContext.SavingsCashAdditions;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<SavingsCashAdditionViewModel>> Handle(CreateSavingsCashAdditionCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsCashAdditionViewModel>();
        var entity = mapper.Map<SavingsCashAddition>(request);


        if (request.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
        {
            var document = mapper.Map<CustomerPaymentDocument>(request);
            document.DocumentType = MemberPaymentUploadType.SAVINGS_DEPOSIT_PAYMENT;

            dbContext.CustomerPaymentDocuments.Add(document);
            entity.CustomerPaymentDocumentId = document.Id;
        }

        dbContext.SavingsCashAdditions.Add(entity);
        await dbContext.SaveChangesAsync();

        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);
        var savingAccount = await dbContext.SavingsAccounts.Include(x => x.DepositProduct)
                             .FirstOrDefaultAsync(c => c.Id == request.SavingsAccountId);


        var approvalRequest = new CreateApprovalModel()
        {
            Module = "DepositProducts",
            Payload = System.Text.Json.JsonSerializer.Serialize(request),
            Comment = "Create Savings Deposit account application approval initiated",
            ApprovalType = ApprovalType.SAVINGS_CASH_ADDITION,
            Description = $"Savings Cash Addition - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName}",
            CreatedBy = request.CreatedByUserId,
            EntityId = entity.Id,
            EntityType = typeof(CreateSavingsCashAdditionCommand),
        };

        var approval = await _approvalService.CreateApproval(approvalRequest, false, savingAccount?.DepositProduct.ApprovalWorkflowId);
        entity.ApprovalId = approval.Response.Id;


        dbContext.SavingsCashAdditions.Update(entity);
        await dbContext.SaveChangesAsync();


        rsp.Response = mapper.Map<SavingsCashAdditionViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<SavingsCashAdditionViewModel>> Handle(UpdateSavingsCashAdditionCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsCashAdditionViewModel>();
        var entity = await dbContext.SavingsCashAdditions.FindAsync(request.Id);


        var account = await dbContext.SavingsAccounts
                         .Include(x => x.LedgerDepositAccount)
                         .FirstOrDefaultAsync(x => x.Id == request.SavingsAccountId);

        var transaction = new DepositTransactionCommand()
        {
            EntityId = entity?.Id,
            EntityType = typeof(SavingsCashAddition),
            IsApproved = false,
            ApprovedOn = DateTime.Now,
            TransactionAction = TransactionAction.POST,
            TransactionDate = DateTime.Now,
            TransactionType = TransactionType.SAVINGS_CASH_ADDITION,
            DepositAccountId = entity?.SavingsAccountId,
            TransactionJournalId = null
        };

        var transactionResponse = await _mediator.Send(transaction);

        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == account.CustomerId);


        var props = new DepositAction
        {
            ActionMessage = "Savings Cash Addition",
            Name = $"{customer?.FirstName} {customer?.LastName}",

        };

        _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_ACTION, customer?.PrimaryEmail, props);

        rsp.Response = mapper.Map<SavingsCashAdditionViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteSavingsCashAdditionCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.SavingsCashAdditions.FindAsync(request.Id);

        dbContext.SavingsCashAdditions.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



