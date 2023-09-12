using AP.ChevronCoop.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ChevronCoop.API.Config;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AuthorizedPermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly string[] _permissions;

    public AuthorizedPermissionAttribute(params string[] permissions)
    {
        _permissions = permissions;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        var userId = context.HttpContext.User.Claims.FirstOrDefault(x => String.Equals(x.Type, JwtRegisteredClaimNames.Sid, StringComparison.CurrentCultureIgnoreCase))?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!_permissions.Any()) return;

        var serviceProvider = context.HttpContext.RequestServices;
        var logger = serviceProvider.GetRequiredService<ILogger<AuthorizedPermissionAttribute>>();
        var dbContext = serviceProvider.GetRequiredService<ChevronCoopDbContext>();

        var userRoles = dbContext.ApplicationUserRoles.Where(x => x.UserId == userId).ToList();

        if (userRoles.Any())
        {

            var roleIds = userRoles.Select(x => x.RoleId).ToList();

            var permissions = dbContext.ApplicationRoleClaims.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.ClaimValue).ToList();

            if (_permissions.Intersect(permissions).Any())
            {
                return;
            }
        }
        context.HttpContext.Items.Add("UserId", userId);


        await context.HttpContext.ForbidAsync();
    }
}