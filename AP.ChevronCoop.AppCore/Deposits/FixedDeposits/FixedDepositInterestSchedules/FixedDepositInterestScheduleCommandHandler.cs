using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestSchedules;

public class FixedDepositInterestScheduleCommandHandler :
      IRequestHandler<QueryFixedDepositInterestScheduleCommand, CommandResult<IQueryable<FixedDepositInterestSchedule>>>,
   IRequestHandler<CreateFixedDepositInterestScheduleCommand, CommandResult<FixedDepositInterestScheduleViewModel>>,
   IRequestHandler<UpdateFixedDepositInterestScheduleCommand, CommandResult<FixedDepositInterestScheduleViewModel>>,
   IRequestHandler<DeleteFixedDepositInterestScheduleCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public FixedDepositInterestScheduleCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<FixedDepositInterestScheduleCommandHandler> _logger, IMapper _mapper)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;

    }


    public Task<CommandResult<IQueryable<FixedDepositInterestSchedule>>> Handle(QueryFixedDepositInterestScheduleCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<FixedDepositInterestSchedule>>();
        rsp.Response = dbContext.FixedDepositInterestSchedules;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<FixedDepositInterestScheduleViewModel>> Handle(CreateFixedDepositInterestScheduleCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositInterestScheduleViewModel>();
        var entity = mapper.Map<FixedDepositInterestSchedule>(request);

        dbContext.FixedDepositInterestSchedules.Add(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<FixedDepositInterestScheduleViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<FixedDepositInterestScheduleViewModel>> Handle(UpdateFixedDepositInterestScheduleCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositInterestScheduleViewModel>();
        var entity = await dbContext.FixedDepositInterestSchedules.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.FixedDepositInterestSchedules.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<FixedDepositInterestScheduleViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteFixedDepositInterestScheduleCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.FixedDepositInterestSchedules.FindAsync(request.Id);

        dbContext.FixedDepositInterestSchedules.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



