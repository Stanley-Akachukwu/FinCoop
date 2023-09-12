using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationRoles
{
    public class ApplicationRoleCommandHandler :
    IRequestHandler<QueryApplicationRoleCommand, CommandResult<IQueryable<ApplicationRole>>>,
    IRequestHandler<GetApplicationRoleCommand, CommandResult<GetApplicationRoleViewModel>>,
    IRequestHandler<CreateApplicationRoleCommand, CommandResult<ApplicationRoleViewModel>>,
    IRequestHandler<UpdateApplicationRoleCommand, CommandResult<ApplicationRoleViewModel>>,
    IRequestHandler<DeleteApplicationRoleCommand, CommandResult<string>>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public ApplicationRoleCommandHandler(ChevronCoopDbContext appDbContext, RoleManager<ApplicationRole> roleManager,
        ILogger<ApplicationRoleCommandHandler> _logger, IMapper _mapper)
        {
            dbContext = appDbContext;
            _roleManager = roleManager;
            logger = _logger;
            mapper = _mapper;
        }

        public Task<CommandResult<IQueryable<ApplicationRole>>> Handle(QueryApplicationRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<ApplicationRole>>();
            rsp.Response = dbContext.ApplicationRoles;
            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<GetApplicationRoleViewModel>> Handle(GetApplicationRoleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<GetApplicationRoleViewModel>();

            var role = await dbContext.ApplicationRoles.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

            if (role is null)
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "Role not found.";
                rsp.ErrorFlag = true;
                return rsp;
            }

            var roleClaims = await dbContext.ApplicationRoleClaims
                .Where(x => x.RoleId == request.Id)
                .Include(x => x.Permission)
                .Select(x => new ApplicationRoleClaimResponse
                {
                    Id = x.PermissionId,
                    Name = x.Permission.Name,
                })
                .ToListAsync(cancellationToken: cancellationToken);

            var response = new GetApplicationRoleViewModel
            {
                Role = mapper.Map<ApplicationRoleViewModel>(role),
                RoleClaims = roleClaims
            };

            rsp.Response = response;
            rsp.StatusCode = (int)HttpStatusCode.OK;
            return rsp;
        }

        public async Task<CommandResult<ApplicationRoleViewModel>> Handle(CreateApplicationRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ApplicationRoleViewModel>();

            // Check permissions
            var permissions = dbContext.Permissions.Where(x => request.PermissionIds.Contains(x.Id)).ToList();
            if (!permissions.Any())
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "Permissions must be specified";
                return rsp;
            }


            var role = new ApplicationRole
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                Name = request.Name,
                Code = request.Code?.Replace(" ", "").ToLower(),
                IsSystemRole = request.IsSystemRole,
                NormalizedName = request.Name
            };

            IdentityResult createRoleResult = await _roleManager.CreateAsync(role);
            if (!createRoleResult.Succeeded)
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = createRoleResult.Errors.FirstOrDefault()?.Description;
                return rsp;
            }

            List<ApplicationRoleClaim> roleClaims = new List<ApplicationRoleClaim>();
            foreach (var permission in permissions)
            {
                roleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = "Permission",
                    PermissionId = permission.Id,
                    ClaimValue = permission.Code
                });
            }

            await dbContext.ApplicationRoleClaims.AddRangeAsync(roleClaims, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = mapper.Map<ApplicationRoleViewModel>(role);
            return rsp;
        }

        public async Task<CommandResult<ApplicationRoleViewModel>> Handle(UpdateApplicationRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ApplicationRoleViewModel>();

            var entity = await dbContext.ApplicationRoles.FindAsync(request.Id);
            if (entity is null)
            {
                rsp.Message = "Application role not found";
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                return rsp;
            }

            try
            {
                // Check permissions
                var permissions = dbContext.Permissions.Where(x => request.PermissionIds.Contains(x.Id)).ToList();
                if (!permissions.Any())
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Permissions must be specified";
                    return rsp;
                }

                var role = await dbContext.ApplicationRoles.Where(x => x.Id == request.Id)
                    .Include(x => x.RoleClaims)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                if (role is null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Selected role does not exist";
                    return rsp;
                }

                role.Name = request.Name;
                role.IsSystemRole = request.IsSystemRole;
                role.Code = request.Code?.Replace(" ", "").ToLower();
                role.NormalizedName = request.Name;

                List<ApplicationRoleClaim> roleClaims = new List<ApplicationRoleClaim>();
                foreach (var permission in permissions)
                {
                    roleClaims.Add(new ApplicationRoleClaim
                    {
                        RoleId = request.Id,
                        ClaimType = "Permission",
                        PermissionId = permission.Id,
                        ClaimValue = permission.Code
                    });
                }
                // TODO: Optimize for performance
                // var claimSet = new HashSet<ApplicationRoleClaim>(roleClaims);
                //
                // var existingClaimSet = new HashSet<ApplicationRoleClaim>(role.RoleClaims);
                // claimSet.ExceptWith(existingClaimSet);
                // existingClaimSet = claimSet;
                //
                // context.ApplicationRoleClaims.UpdateRange(claimSet);
                dbContext.RoleClaims.RemoveRange(role.RoleClaims);

                role.RoleClaims = roleClaims;


                dbContext.ApplicationRoles.Update(role);

                await dbContext.SaveChangesAsync(cancellationToken);

                rsp.Response = mapper.Map<ApplicationRoleViewModel>(role);
                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.ErrorFlag = false;
                return rsp;
            }
            catch (Exception e)
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = e.Message;
                rsp.ErrorFlag = true;
                return rsp;
            }
        }

        public async Task<CommandResult<string>> Handle(DeleteApplicationRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();

            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "Role does not exist";
                return rsp;
            }

            if (role.IsSystemRole)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "System roles cannot be deleted";
                return rsp;
            }

            var deleteRoleResult = await _roleManager.DeleteAsync(role);
            if (!deleteRoleResult.Succeeded)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = deleteRoleResult.Errors.FirstOrDefault()?.Description;
                return rsp;
            }

            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = "Data successfully deleted";
            return rsp;
        }
    }
}
