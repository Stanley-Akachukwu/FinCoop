
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProductCharges
{
    public class DepositProductChargeCommandHandler :
      IRequestHandler<QueryDepositProductChargeCommand, CommandResult<IQueryable<DepositProductCharge>>>,
   IRequestHandler<CreateDepositProductChargeCommand, CommandResult<DepositProductChargeViewModel>>,
   IRequestHandler<UpdateDepositProductChargeCommand, CommandResult<DepositProductChargeViewModel>>,
   IRequestHandler<DeleteDepositProductChargeCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public DepositProductChargeCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<DepositProductChargeCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }
        public Task<CommandResult<IQueryable<DepositProductCharge>>> Handle(QueryDepositProductChargeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<DepositProductCharge>>();
            rsp.Response = dbContext.DepositProductCharges;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<DepositProductChargeViewModel>> Handle(CreateDepositProductChargeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<DepositProductChargeViewModel>();
            var entity = mapper.Map<DepositProductCharge>(request);

            dbContext.DepositProductCharges.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<DepositProductChargeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<DepositProductChargeViewModel>> Handle(UpdateDepositProductChargeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<DepositProductChargeViewModel>();
            var entity = await dbContext.DepositProductCharges.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.DepositProductCharges.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<DepositProductChargeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteDepositProductChargeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.DepositProductCharges.FindAsync(request.Id);

            dbContext.DepositProductCharges.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }

    }

}

