using AP.ChevronCoop.AppDomain.MasterData.GlobalCodes;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.GlobalCodes
{
    public class GlobalCodeCommandHandler :
     IRequestHandler<QueryGlobalCodeCommand, CommandResult<IQueryable<GlobalCode>>>,
    IRequestHandler<CreateGlobalCodeCommand, CommandResult<GlobalCodeViewModel>>,
    IRequestHandler<UpdateGlobalCodeCommand, CommandResult<GlobalCodeViewModel>>,
    IRequestHandler<DeleteGlobalCodeCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public GlobalCodeCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<GlobalCodeCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<GlobalCode>>> Handle(QueryGlobalCodeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<GlobalCode>>();
            rsp.Response = dbContext.GlobalCodes;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<GlobalCodeViewModel>> Handle(CreateGlobalCodeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<GlobalCodeViewModel>();
            var entity = mapper.Map<GlobalCode>(request);

            dbContext.GlobalCodes.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<GlobalCodeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<GlobalCodeViewModel>> Handle(UpdateGlobalCodeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<GlobalCodeViewModel>();
            var entity = await dbContext.GlobalCodes.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.GlobalCodes.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<GlobalCodeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteGlobalCodeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.GlobalCodes.FindAsync(request.Id);

            dbContext.GlobalCodes.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
