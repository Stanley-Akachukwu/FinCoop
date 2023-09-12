using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;

public class FixedDepositInterestScheduleItemCommandHandler :
      IRequestHandler<QueryFixedDepositInterestScheduleItemCommand, CommandResult<IQueryable<FixedDepositInterestScheduleItem>>>,
   IRequestHandler<CreateFixedDepositInterestScheduleItemCommand, CommandResult<FixedDepositInterestScheduleItemViewModel>>,
   IRequestHandler<UpdateFixedDepositInterestScheduleItemCommand, CommandResult<FixedDepositInterestScheduleItemViewModel>>,
   IRequestHandler<DeleteFixedDepositInterestScheduleItemCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public FixedDepositInterestScheduleItemCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<FixedDepositInterestScheduleItemCommandHandler> _logger, IMapper _mapper)
    {

        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;

    }


    public Task<CommandResult<IQueryable<FixedDepositInterestScheduleItem>>> Handle(QueryFixedDepositInterestScheduleItemCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<IQueryable<FixedDepositInterestScheduleItem>>();
        rsp.Response = dbContext.FixedDepositInterestScheduleItems;

        return Task.FromResult(rsp);
    }




    public async Task<CommandResult<FixedDepositInterestScheduleItemViewModel>> Handle(CreateFixedDepositInterestScheduleItemCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositInterestScheduleItemViewModel>();
        var entity = mapper.Map<FixedDepositInterestScheduleItem>(request);

        dbContext.FixedDepositInterestScheduleItems.Add(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<FixedDepositInterestScheduleItemViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<FixedDepositInterestScheduleItemViewModel>> Handle(UpdateFixedDepositInterestScheduleItemCommand request, CancellationToken cancellationToken)
    {

        var rsp = new CommandResult<FixedDepositInterestScheduleItemViewModel>();
        var entity = await dbContext.FixedDepositInterestScheduleItems.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.FixedDepositInterestScheduleItems.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<FixedDepositInterestScheduleItemViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteFixedDepositInterestScheduleItemCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.FixedDepositInterestScheduleItems.FindAsync(request.Id);

        dbContext.FixedDepositInterestScheduleItems.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}



