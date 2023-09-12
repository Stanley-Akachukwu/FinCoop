

using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestAdditions
{
    public class SpecialDepositInterestAdditionCommandHandler :
	  IRequestHandler<QuerySpecialDepositInterestAdditionCommand, CommandResult<IQueryable<SpecialDepositInterestAddition>>>,
   IRequestHandler<CreateSpecialDepositInterestAdditionCommand, CommandResult<SpecialDepositInterestAdditionViewModel>>,
   IRequestHandler<UpdateSpecialDepositInterestAdditionCommand, CommandResult<SpecialDepositInterestAdditionViewModel>>,
   IRequestHandler<DeleteSpecialDepositInterestAdditionCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IMediator _mediator;

        public SpecialDepositInterestAdditionCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<SpecialDepositInterestAdditionCommandHandler> _logger, IMapper _mapper, IMediator mediator)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
            _mediator = mediator;

        }


        public Task<CommandResult<IQueryable<SpecialDepositInterestAddition>>> Handle(QuerySpecialDepositInterestAdditionCommand request, CancellationToken cancellationToken)
        {
			var rsp = new CommandResult<IQueryable<SpecialDepositInterestAddition>>();
            rsp.Response = dbContext.SpecialDepositInterestAdditions;

            return Task.FromResult(rsp);
        }
		



        public async Task<CommandResult<SpecialDepositInterestAdditionViewModel>> Handle(CreateSpecialDepositInterestAdditionCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<SpecialDepositInterestAdditionViewModel>();
            var entity = mapper.Map<SpecialDepositInterestAddition>(request);
            entity.ProcessedDate = DateTime.Now;
            entity.IsProcessed = true;
            entity.Caption = $"Special Deposit Account Interest Addition";
            entity.InterestScheduleItem = dbContext.SpecialDepositInterestScheduleItems.Where(x => x.Id == request.InterestScheduleItemId).FirstOrDefault();

            dbContext.SpecialDepositInterestAdditions.Add(entity);
            await dbContext.SaveChangesAsync();

            var transaction = new DepositTransactionCommand()
            {
                EntityId = entity.Id,
                EntityType = typeof(SpecialDepositInterestAddition),
                IsApproved = true,
                ApprovedOn = DateTime.Now,
                TransactionAction = TransactionAction.POST,
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.SPECIAL_DEPOSIT_INTEREST_ADDITION,
                DepositAccountId = entity.SpecialDepositAccountId,
                TransactionJournalId = null
            };
            var transactionResponse = await _mediator.Send(transaction);

            rsp.Response = mapper.Map<SpecialDepositInterestAdditionViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<SpecialDepositInterestAdditionViewModel>> Handle(UpdateSpecialDepositInterestAdditionCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositInterestAdditionViewModel>();
            var entity = await dbContext.SpecialDepositInterestAdditions.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.SpecialDepositInterestAdditions.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositInterestAdditionViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositInterestAdditionCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.SpecialDepositInterestAdditions.FindAsync(request.Id);

            dbContext.SpecialDepositInterestAdditions.Remove(entity);
            await dbContext.SaveChangesAsync();
           
            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }
    }



