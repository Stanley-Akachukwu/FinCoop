using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserTokens;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserTokens;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserTokens
{
    public class ApplicationUserTokenCommandHandler :
     IRequestHandler<QueryApplicationUserTokenCommand, CommandResult<IQueryable<ApplicationUserToken>>>,
    IRequestHandler<CreateApplicationUserTokenCommand, CommandResult<ApplicationUserTokenViewModel>>,
    IRequestHandler<UpdateApplicationUserTokenCommand, CommandResult<ApplicationUserTokenViewModel>>,
    IRequestHandler<DeleteApplicationUserTokenCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ApplicationUserTokenCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<ApplicationUserTokenCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<ApplicationUserToken>>> Handle(QueryApplicationUserTokenCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<ApplicationUserToken>>();
            rsp.Response = dbContext.ApplicationUserTokens;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<ApplicationUserTokenViewModel>> Handle(CreateApplicationUserTokenCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApplicationUserTokenViewModel>();
            var entity = mapper.Map<ApplicationUserToken>(request);

            dbContext.ApplicationUserTokens.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApplicationUserTokenViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<ApplicationUserTokenViewModel>> Handle(UpdateApplicationUserTokenCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApplicationUserTokenViewModel>();
            var entity = await dbContext.ApplicationUserTokens.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.ApplicationUserTokens.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApplicationUserTokenViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteApplicationUserTokenCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.ApplicationUserTokens.FindAsync(request.Id);

            dbContext.ApplicationUserTokens.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }






}
