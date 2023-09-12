using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserClaims;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserClaims;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserClaims
{
    public class ApplicationUserClaimCommandHandler :
     IRequestHandler<QueryApplicationUserClaimCommand, CommandResult<IQueryable<ApplicationUserClaim>>>,
    IRequestHandler<CreateApplicationUserClaimCommand, CommandResult<ApplicationUserClaimViewModel>>,
    IRequestHandler<UpdateApplicationUserClaimCommand, CommandResult<ApplicationUserClaimViewModel>>,
    IRequestHandler<DeleteApplicationUserClaimCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ApplicationUserClaimCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<ApplicationUserClaimCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<ApplicationUserClaim>>> Handle(QueryApplicationUserClaimCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<ApplicationUserClaim>>();
            rsp.Response = dbContext.ApplicationUserClaims;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<ApplicationUserClaimViewModel>> Handle(CreateApplicationUserClaimCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApplicationUserClaimViewModel>();
            var entity = mapper.Map<ApplicationUserClaim>(request);

            dbContext.ApplicationUserClaims.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApplicationUserClaimViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<ApplicationUserClaimViewModel>> Handle(UpdateApplicationUserClaimCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApplicationUserClaimViewModel>();
            var entity = await dbContext.ApplicationUserClaims.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.ApplicationUserClaims.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApplicationUserClaimViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteApplicationUserClaimCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.ApplicationUserClaims.FindAsync(request.Id);

            dbContext.ApplicationUserClaims.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }






}
