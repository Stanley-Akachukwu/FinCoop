

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{
    public class SpecialDepositInterestScheduleCommandHandler :
	  IRequestHandler<QuerySpecialDepositInterestScheduleCommand, CommandResult<IQueryable<SpecialDepositInterestSchedule>>>,
   IRequestHandler<CreateSpecialDepositInterestScheduleCommand, CommandResult<SpecialDepositInterestScheduleViewModel>>,
   IRequestHandler<UpdateSpecialDepositInterestScheduleCommand, CommandResult<SpecialDepositInterestScheduleViewModel>>,
   IRequestHandler<DeleteSpecialDepositInterestScheduleCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public SpecialDepositInterestScheduleCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<SpecialDepositInterestScheduleCommandHandler> _logger, IMapper _mapper)
        {
            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
        }


		public Task<CommandResult<IQueryable<SpecialDepositInterestSchedule>>> Handle(QuerySpecialDepositInterestScheduleCommand request, CancellationToken cancellationToken)
        {
			var rsp = new CommandResult<IQueryable<SpecialDepositInterestSchedule>>();
            rsp.Response = dbContext.SpecialDepositInterestSchedules;
            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<SpecialDepositInterestScheduleViewModel>> Handle(CreateSpecialDepositInterestScheduleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositInterestScheduleViewModel>();
            var entity = mapper.Map<SpecialDepositInterestSchedule>(request);

            dbContext.SpecialDepositInterestSchedules.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositInterestScheduleViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<SpecialDepositInterestScheduleViewModel>> Handle(UpdateSpecialDepositInterestScheduleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositInterestScheduleViewModel>();
            var entity = await dbContext.SpecialDepositInterestSchedules.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.SpecialDepositInterestSchedules.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositInterestScheduleViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositInterestScheduleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.SpecialDepositInterestSchedules.FindAsync(request.Id);

            dbContext.SpecialDepositInterestSchedules.Remove(entity);
            await dbContext.SaveChangesAsync();
           
            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }
	
    }


