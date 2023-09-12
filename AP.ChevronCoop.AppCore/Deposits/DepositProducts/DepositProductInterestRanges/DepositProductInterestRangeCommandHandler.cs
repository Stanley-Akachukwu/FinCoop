using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProductInterestRanges
{

    public class DepositProductInterestRangeCommandHandler :
      IRequestHandler<QueryDepositProductInterestRangeCommand, CommandResult<IQueryable<DepositProductInterestRange>>>,
   IRequestHandler<CreateDepositProductInterestRangeCommand, CommandResult<DepositProductInterestRangeViewModel>>,
   IRequestHandler<UpdateDepositProductInterestRangeCommand, CommandResult<DepositProductInterestRangeViewModel>>,
   IRequestHandler<DeleteDepositProductInterestRangeCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public DepositProductInterestRangeCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<DepositProductInterestRangeCommandHandler> _logger, IMapper _mapper)
        {
            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
        }

        public Task<CommandResult<IQueryable<DepositProductInterestRange>>> Handle(QueryDepositProductInterestRangeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<DepositProductInterestRange>>();
            rsp.Response = dbContext.DepositProductInterestRanges;
            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<DepositProductInterestRangeViewModel>> Handle(CreateDepositProductInterestRangeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<DepositProductInterestRangeViewModel>();
            var entity = mapper.Map<DepositProductInterestRange>(request);

            dbContext.DepositProductInterestRanges.Add(entity);
            await dbContext.SaveChangesAsync();
            rsp.Response = mapper.Map<DepositProductInterestRangeViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<DepositProductInterestRangeViewModel>> Handle(UpdateDepositProductInterestRangeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<DepositProductInterestRangeViewModel>();
            var entity = await dbContext.DepositProductInterestRanges.FindAsync(request.Id);
            mapper.Map(request, entity);
            dbContext.DepositProductInterestRanges.Update(entity);
            await dbContext.SaveChangesAsync();
            rsp.Response = mapper.Map<DepositProductInterestRangeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteDepositProductInterestRangeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.DepositProductInterestRanges.FindAsync(request.Id);

            dbContext.DepositProductInterestRanges.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }

        //public Task<CommandResult<IQueryable<DepositProductInterestRange>>> Handle(QueryDepositProductInterestRangeCommand request, CancellationToken cancellationToken)
        //{

        //    var rsp = new CommandResult<IQueryable<DepositProductInterestRange>>();
        //    rsp.Response = dbContext.DepositProductInterestRanges;

        //    return Task.FromResult(rsp);
        //}




        //public async Task<CommandResult<DepositProductInterestRangeViewModel>> Handle(CreateDepositProductInterestRangeCommand request, CancellationToken cancellationToken)
        //{

        //    var rsp = new CommandResult<DepositProductInterestRangeViewModel>();
        //    var entity = mapper.Map<DepositProductInterestRange>(request);

        //    dbContext.DepositProductInterestRanges.Add(entity);
        //    await dbContext.SaveChangesAsync();

        //    rsp.Response = mapper.Map<DepositProductInterestRangeViewModel>(entity);

        //    return rsp;
        //}

        //public async Task<CommandResult<DepositProductInterestRangeViewModel>> Handle(UpdateDepositProductInterestRangeCommand request, CancellationToken cancellationToken)
        //{

        //    var rsp = new CommandResult<DepositProductInterestRangeViewModel>();
        //    var entity = await dbContext.DepositProductInterestRanges.FindAsync(request.Id);

        //    mapper.Map(request, entity);

        //    dbContext.DepositProductInterestRanges.Update(entity);
        //    await dbContext.SaveChangesAsync();

        //    rsp.Response = mapper.Map<DepositProductInterestRangeViewModel>(entity);

        //    return rsp;
        //}

        //public async Task<CommandResult<string>> Handle(DeleteDepositProductInterestRangeCommand request, CancellationToken cancellationToken)
        //{
        //    var rsp = new CommandResult<string>();
        //    var entity = await dbContext.DepositProductInterestRanges.FindAsync(request.Id);

        //    dbContext.DepositProductInterestRanges.Remove(entity);
        //    await dbContext.SaveChangesAsync();

        //    rsp.Response = "Data successfully deleted";

        //    return rsp;
        //}
    }

}


