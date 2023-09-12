

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules
{
    public class SpecialDepositAccountDeductionScheduleCommandHandler :
	  IRequestHandler<QuerySpecialDepositAccountDeductionScheduleCommand, CommandResult<IQueryable<SpecialDepositAccountDeductionSchedule>>>,
   IRequestHandler<CreateSpecialDepositAccountDeductionScheduleCommand, CommandResult<SpecialDepositAccountDeductionScheduleViewModel>>,
   IRequestHandler<UpdateSpecialDepositAccountDeductionScheduleCommand, CommandResult<SpecialDepositAccountDeductionScheduleViewModel>>,
   IRequestHandler<DeleteSpecialDepositAccountDeductionScheduleCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public SpecialDepositAccountDeductionScheduleCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<SpecialDepositAccountDeductionScheduleCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


		public Task<CommandResult<IQueryable<SpecialDepositAccountDeductionSchedule>>> Handle(QuerySpecialDepositAccountDeductionScheduleCommand request, CancellationToken cancellationToken)
        {

			var rsp = new CommandResult<IQueryable<SpecialDepositAccountDeductionSchedule>>();
            rsp.Response = dbContext.SpecialDepositAccountDeductionSchedules;

            return Task.FromResult(rsp);
        }
		



        public async Task<CommandResult<SpecialDepositAccountDeductionScheduleViewModel>> Handle(CreateSpecialDepositAccountDeductionScheduleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositAccountDeductionScheduleViewModel>();
            var entity = mapper.Map<SpecialDepositAccountDeductionSchedule>(request);

            dbContext.SpecialDepositAccountDeductionSchedules.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositAccountDeductionScheduleViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<SpecialDepositAccountDeductionScheduleViewModel>> Handle(UpdateSpecialDepositAccountDeductionScheduleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<SpecialDepositAccountDeductionScheduleViewModel>();
            var entity = await dbContext.SpecialDepositAccountDeductionSchedules.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.SpecialDepositAccountDeductionSchedules.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<SpecialDepositAccountDeductionScheduleViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteSpecialDepositAccountDeductionScheduleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.SpecialDepositAccountDeductionSchedules.FindAsync(request.Id);

            dbContext.SpecialDepositAccountDeductionSchedules.Remove(entity);
            await dbContext.SaveChangesAsync();
           
            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }
	
    }


