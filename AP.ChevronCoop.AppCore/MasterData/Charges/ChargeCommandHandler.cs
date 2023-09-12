using AP.ChevronCoop.AppDomain.MasterData.Charges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Charges
{
    public class ChargeCommandHandler :
     IRequestHandler<QueryChargeCommand, CommandResult<IQueryable<Charge>>>,
    IRequestHandler<CreateChargeCommand, CommandResult<ChargeViewModel>>,
    IRequestHandler<UpdateChargeCommand, CommandResult<ChargeViewModel>>,
    IRequestHandler<DeleteChargeCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ChargeCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<ChargeCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<Charge>>> Handle(QueryChargeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<Charge>>();
            rsp.Response = dbContext.Charges;
            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<ChargeViewModel>> Handle(CreateChargeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ChargeViewModel>();
            var entity = mapper.Map<Charge>(request);

            dbContext.Charges.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ChargeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<ChargeViewModel>> Handle(UpdateChargeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ChargeViewModel>();
            var entity = await dbContext.Charges.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.Charges.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ChargeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteChargeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.Charges.FindAsync(request.Id);

            dbContext.Charges.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
