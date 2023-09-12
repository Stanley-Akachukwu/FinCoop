using AP.ChevronCoop.AppDomain.Security.Auth.Permissions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.Permissions
{
    public class PermissionCommandHandler :
     IRequestHandler<QueryPermissionCommand, CommandResult<IQueryable<Permission>>>,
    IRequestHandler<CreatePermissionCommand, CommandResult<PermissionViewModel>>,
    IRequestHandler<UpdatePermissionCommand, CommandResult<PermissionViewModel>>,
    IRequestHandler<DeletePermissionCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public PermissionCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<PermissionCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<Permission>>> Handle(QueryPermissionCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<Permission>>();
            rsp.Response = dbContext.Permissions;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<PermissionViewModel>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<PermissionViewModel>();
            var entity = mapper.Map<Permission>(request);

            dbContext.Permissions.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<PermissionViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<PermissionViewModel>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<PermissionViewModel>();
            var entity = await dbContext.Permissions.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.Permissions.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<PermissionViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.Permissions.FindAsync(request.Id);

            dbContext.Permissions.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }






}
