
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccountDeductionSchedules;
public class SavingsAccountDeductionScheduleCommandHandler :
      IRequestHandler<QuerySavingsAccountDeductionScheduleCommand, CommandResult<IQueryable<SavingsAccountDeductionSchedule>>>,
   IRequestHandler<CreateSavingsAccountDeductionScheduleCommand, CommandResult<SavingsAccountDeductionScheduleViewModel>>,
   IRequestHandler<UpdateSavingsAccountDeductionScheduleCommand, CommandResult<SavingsAccountDeductionScheduleViewModel>>,
   IRequestHandler<DeleteSavingsAccountDeductionScheduleCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public SavingsAccountDeductionScheduleCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<SavingsAccountDeductionScheduleCommandHandler> _logger, IMapper _mapper)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;

    }


    public Task<CommandResult<IQueryable<SavingsAccountDeductionSchedule>>> Handle(QuerySavingsAccountDeductionScheduleCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<SavingsAccountDeductionSchedule>>();
        rsp.Response = dbContext.SavingsAccountDeductionSchedules;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<SavingsAccountDeductionScheduleViewModel>> Handle(CreateSavingsAccountDeductionScheduleCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsAccountDeductionScheduleViewModel>();
        var entity = mapper.Map<SavingsAccountDeductionSchedule>(request);

        dbContext.SavingsAccountDeductionSchedules.Add(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<SavingsAccountDeductionScheduleViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<SavingsAccountDeductionScheduleViewModel>> Handle(UpdateSavingsAccountDeductionScheduleCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<SavingsAccountDeductionScheduleViewModel>();
        var entity = await dbContext.SavingsAccountDeductionSchedules.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.SavingsAccountDeductionSchedules.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<SavingsAccountDeductionScheduleViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteSavingsAccountDeductionScheduleCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.SavingsAccountDeductionSchedules.FindAsync(request.Id);

        dbContext.SavingsAccountDeductionSchedules.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}




