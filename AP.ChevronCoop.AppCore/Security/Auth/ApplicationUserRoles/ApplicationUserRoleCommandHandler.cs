using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using AP.ChevronCoop.Entities.Security;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserRoles
{
    public class ApplicationUserRoleCommandHandler :
     IRequestHandler<QueryApplicationUserRoleCommand, CommandResult<IQueryable<ApplicationUserRole>>>,
    IRequestHandler<CreateApplicationUserRoleCommand, CommandResult<List<ApplicationUserRoleViewModel>>>,
    IRequestHandler<UpdateApplicationUserRoleCommand, CommandResult<List<ApplicationUserRoleViewModel>>>,
    IRequestHandler<DeleteApplicationUserRoleCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ApplicationUserRoleCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<ApplicationUserRoleCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<ApplicationUserRole>>> Handle(QueryApplicationUserRoleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<ApplicationUserRole>>();
            rsp.Response = dbContext.ApplicationUserRoles;

            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<List<ApplicationUserRoleViewModel>>> Handle(CreateApplicationUserRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<List<ApplicationUserRoleViewModel>>();
            if (!request.RoleId.Any())
            {
                rsp.ErrorFlag = true;
                rsp.Message = "Minimum of a role must be specified";
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                return rsp;
            }

            var entities = new List<ApplicationUserRole>();

            foreach (var roleId in request.RoleId)
            {
                entities.Add(new ApplicationUserRole
                {
                    RoleId = roleId,
                    UserId = request.UserId
                });
            }

            await dbContext.ApplicationUserRoles.AddRangeAsync(entities, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = mapper.Map<List<ApplicationUserRoleViewModel>>(entities);

            return rsp;
        }

        public async Task<CommandResult<List<ApplicationUserRoleViewModel>>> Handle(UpdateApplicationUserRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<List<ApplicationUserRoleViewModel>>();
            if (!request.RoleId.Any())
            {
                rsp.ErrorFlag = true;
                rsp.Message = "Minimum of a role must be specified";
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                return rsp;
            }

            var userRoleQuery = dbContext.ApplicationUserRoles.Where(x => x.UserId == request.UserId);
            
            // Get the existing roles Id
            var userRoles =  userRoleQuery.Select(x => x.RoleId).ToHashSet();

            // Get the roles to be removed
            var rolesToRemoveId = userRoles.Except(request.RoleId).ToHashSet();
            var rolesToRemove = userRoleQuery.Where(x => rolesToRemoveId.Contains(x.RoleId));
            dbContext.ApplicationUserRoles.RemoveRange(rolesToRemove);

            // Get the roles to be added
            var rolesToAdd = request.RoleId.Except(userRoles).ToHashSet();
            
            var entities = new List<ApplicationUserRole>();
            foreach (var roleId in rolesToAdd)
            {
                entities.Add(new ApplicationUserRole
                {
                    RoleId = roleId,
                    UserId = request.UserId
                });
            }
            await dbContext.ApplicationUserRoles.AddRangeAsync(entities, cancellationToken);

            if (rolesToAdd.Any())
            {
                // get roles id for regular, expatriate and retiree
                var memberRoles = new List<string>() { MemberType.REGULAR.ToString().ToLower(), MemberType.RETIREE.ToString().ToLower(), MemberType.EXPATRIATE.ToString().ToLower() };
                var regularRoleId = await dbContext.Roles.Where(x => memberRoles.Contains(x.Code.ToLower()))
                    .Select(x => x.Id).ToListAsync(cancellationToken: cancellationToken);

                if (rolesToAdd.Except(regularRoleId).Any())
                {
                    var user = await dbContext.ApplicationUsers.FindAsync(request.UserId);
                    user.IsAdmin = true;
                    dbContext.ApplicationUsers.Update(user);
                }
            }
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = mapper.Map<List<ApplicationUserRoleViewModel>>(entities);
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteApplicationUserRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.ApplicationUserRoles.FindAsync(request.Id);

            dbContext.ApplicationUserRoles.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }
}
