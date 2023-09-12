using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationRoleClaims
{
    public class ApplicationRoleClaimCommandHandler :
     IRequestHandler<QueryApplicationRoleClaimCommand, CommandResult<IQueryable<ApplicationRoleClaim>>>,
    IRequestHandler<CreateApplicationRoleClaimCommand, CommandResult<ApplicationRoleClaimViewModel>>,
    IRequestHandler<UpdateApplicationRoleClaimCommand, CommandResult<ApplicationRoleClaimViewModel>>,
    IRequestHandler<DeleteApplicationRoleClaimCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ApplicationRoleClaimCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<ApplicationRoleClaimCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<ApplicationRoleClaim>>> Handle(QueryApplicationRoleClaimCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<ApplicationRoleClaim>>();
            rsp.Response = dbContext.ApplicationRoleClaims;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<ApplicationRoleClaimViewModel>> Handle(CreateApplicationRoleClaimCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApplicationRoleClaimViewModel>();
            var entity = mapper.Map<ApplicationRoleClaim>(request);

            dbContext.ApplicationRoleClaims.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApplicationRoleClaimViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<ApplicationRoleClaimViewModel>> Handle(UpdateApplicationRoleClaimCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApplicationRoleClaimViewModel>();
            var entity = await dbContext.ApplicationRoleClaims.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.ApplicationRoleClaims.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApplicationRoleClaimViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteApplicationRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.ApplicationRoleClaims.FindAsync(request.Id);

            dbContext.ApplicationRoleClaims.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }






}
