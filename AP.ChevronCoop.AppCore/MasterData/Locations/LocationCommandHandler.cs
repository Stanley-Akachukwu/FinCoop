using AP.ChevronCoop.AppDomain.MasterData.Locations;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Locations;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Locations
{
    public class LocationCommandHandler :
     IRequestHandler<QueryLocationCommand, CommandResult<IQueryable<Location>>>,
    IRequestHandler<CreateLocationCommand, CommandResult<LocationViewModel>>,
    IRequestHandler<UpdateLocationCommand, CommandResult<LocationViewModel>>,
    IRequestHandler<DeleteLocationCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public LocationCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<LocationCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<Location>>> Handle(QueryLocationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<Location>>();
            rsp.Response = dbContext.Locations;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<LocationViewModel>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<LocationViewModel>();
            var entity = mapper.Map<Location>(request);

            dbContext.Locations.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<LocationViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<LocationViewModel>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<LocationViewModel>();
            var entity = await dbContext.Locations.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.Locations.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<LocationViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.Locations.FindAsync(request.Id);

            dbContext.Locations.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
