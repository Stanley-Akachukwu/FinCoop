

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public class SpecialDepositInterestScheduleItemCommandHandler :
	  IRequestHandler<QuerySpecialDepositInterestScheduleItemCommand, CommandResult<IQueryable<SpecialDepositInterestScheduleItem>>>,
   IRequestHandler<CreateSpecialDepositInterestScheduleItemCommand, CommandResult<SpecialDepositInterestScheduleItemViewModel>>,
   IRequestHandler<UpdateSpecialDepositInterestScheduleItemCommand, CommandResult<SpecialDepositInterestScheduleItemViewModel>>,
   IRequestHandler<DeleteSpecialDepositInterestScheduleItemCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public SpecialDepositInterestScheduleItemCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<SpecialDepositInterestScheduleItemCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }

		public Task<CommandResult<IQueryable<SpecialDepositInterestScheduleItem>>> Handle(QuerySpecialDepositInterestScheduleItemCommand request, CancellationToken cancellationToken)
        {
			var rsp = new CommandResult<IQueryable<SpecialDepositInterestScheduleItem>>();
            rsp.Response = dbContext.SpecialDepositInterestScheduleItems;
            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<SpecialDepositInterestScheduleItemViewModel>> Handle(CreateSpecialDepositInterestScheduleItemCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositInterestScheduleItemViewModel>();
            var entity = mapper.Map<SpecialDepositInterestScheduleItem>(request);

            dbContext.SpecialDepositInterestScheduleItems.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositInterestScheduleItemViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<SpecialDepositInterestScheduleItemViewModel>> Handle(UpdateSpecialDepositInterestScheduleItemCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositInterestScheduleItemViewModel>();
            var entity = await dbContext.SpecialDepositInterestScheduleItems.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.SpecialDepositInterestScheduleItems.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositInterestScheduleItemViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositInterestScheduleItemCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.SpecialDepositInterestScheduleItems.FindAsync(request.Id);

            dbContext.SpecialDepositInterestScheduleItems.Remove(entity);
            await dbContext.SaveChangesAsync();
           
            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }

}


