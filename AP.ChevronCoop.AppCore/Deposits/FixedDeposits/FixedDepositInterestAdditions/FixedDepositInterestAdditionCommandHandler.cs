using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestAdditions;

public class FixedDepositInterestAdditionCommandHandler :
      IRequestHandler<QueryFixedDepositInterestAdditionCommand, CommandResult<IQueryable<FixedDepositInterestAddition>>>,
   IRequestHandler<CreateFixedDepositInterestAdditionCommand, CommandResult<FixedDepositInterestAdditionViewModel>>,
   IRequestHandler<UpdateFixedDepositInterestAdditionCommand, CommandResult<FixedDepositInterestAdditionViewModel>>,
   IRequestHandler<DeleteFixedDepositInterestAdditionCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IMediator _mediator;

    public FixedDepositInterestAdditionCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<FixedDepositInterestAdditionCommandHandler> _logger, IMapper _mapper , IMediator mediator)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _mediator = mediator;

    }


    public Task<CommandResult<IQueryable<FixedDepositInterestAddition>>> Handle(QueryFixedDepositInterestAdditionCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<FixedDepositInterestAddition>>();
        rsp.Response = dbContext.FixedDepositInterestAdditions;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<FixedDepositInterestAdditionViewModel>> Handle(CreateFixedDepositInterestAdditionCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositInterestAdditionViewModel>();
        var entity = mapper.Map<FixedDepositInterestAddition>(request);

        dbContext.FixedDepositInterestAdditions.Add(entity);
        await dbContext.SaveChangesAsync();

        var transaction = new DepositTransactionCommand()
        {
            EntityId = entity.Id,
            EntityType = typeof(FixedDepositInterestAddition),
            IsApproved = false,
            ApprovedOn = DateTime.Now,
            TransactionAction = TransactionAction.POST,
            TransactionDate = DateTime.Now,
            TransactionType = TransactionType.FIXED_DEPOSIT_INTEREST_ADDITION,
            DepositAccountId = entity.Id,
            TransactionJournalId = null
        };

        var transactionResponse = await _mediator.Send(transaction);



        rsp.Response = mapper.Map<FixedDepositInterestAdditionViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<FixedDepositInterestAdditionViewModel>> Handle(UpdateFixedDepositInterestAdditionCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositInterestAdditionViewModel>();
        var entity = await dbContext.FixedDepositInterestAdditions.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.FixedDepositInterestAdditions.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<FixedDepositInterestAdditionViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteFixedDepositInterestAdditionCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.FixedDepositInterestAdditions.FindAsync(request.Id);

        dbContext.FixedDepositInterestAdditions.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



