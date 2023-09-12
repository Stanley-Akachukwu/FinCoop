using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AP.ChevronCoop.AppCore.Services.AuditServices;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChevronCoop.API.CustomFilters;

public class ChevronCoopLogInterceptor<T> : IAsyncActionFilter where T : class
{
    private static readonly string className = "ChevronCoopLogInterceptor";
    private readonly IAuditService _auditLog;
    private readonly ChevronCoopDbContext _dbContext;

    private readonly ILoggerService _logger;

    public ChevronCoopLogInterceptor(ILoggerService logger, IAuditService auditLog, ChevronCoopDbContext dbContext)
    {
        _logger = logger;
        _auditLog = auditLog;
        _dbContext = dbContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionPerformed = "";
        var description = "";
        var module = "NA";
        var createdByUserId = "";
        var payload = "";
        var username = "";

        var methodName = "OnActionExecutionAsync";
        var contextIype = typeof(T);
        var requestContext = context.HttpContext;
        var actionName = context.HttpContext.Request.RouteValues["action"].ToString();
        var controllerName = context.HttpContext.Request.RouteValues["controller"].ToString();
        var reader = new StreamReader(requestContext.Request.Body, Encoding.UTF8);
        payload = await reader.ReadToEndAsync();
        module = contextIype.FullName;
        var resource = contextIype.Name;
        resource = resource.Remove(0, 5);
        resource = resource.Remove(resource.Length - 7);
        actionPerformed = $"Accessing {resource} database table.";
        description = $"Accessed {resource} database table to browse the list of {resource}s";
        var bearerToken = "";

        var ishttpPost = context.HttpContext.Request.Method == "POST";


        payload = ishttpPost ? payload : "NA";
        try
        {
            if (ishttpPost)
                bearerToken = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!ishttpPost)
            {
                context.HttpContext.Request.Headers.TryGetValue("Bearer", out var authorization);
                bearerToken = authorization.ToString().Replace("Bearer ", "");
            }

            var claims = await GetClaimsAsync(bearerToken);
            if (string.IsNullOrEmpty(claims.Item1) || string.IsNullOrEmpty(claims.Item2))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            createdByUserId = claims.Item2;

            var memberUser = _dbContext.MemberProfiles.FirstOrDefault(x => x.ApplicationUserId == createdByUserId);

            if (memberUser == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            username = $"{memberUser.PrimaryEmail}({memberUser.MembershipId})";
            if (!ishttpPost)
                await _auditLog.LogAudit(actionPerformed, description, module, createdByUserId, payload, username);

            await _logger.LogInfo(className, methodName,
                $"Intercepting request for {controllerName}Controller.{actionName} and Blazor client request payload:\r\n" +
                payload);
            context.HttpContext.Items.Add("CurrentUser", memberUser);
        }
        catch (Exception ex)
        {
            await _logger.LogError(className, methodName, ex);
            context.Result = new BadRequestResult();
            return;
        }

        next();
    }

    private async Task<(string, string)> GetClaimsAsync(string token)
    {
        var claimResult = ("", "");
        var stream = token;
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(stream);
        var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

        var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
        var id = tokenS.Claims.First(claim => claim.Type == "sid").Value;
        claimResult = (email, id);
        return await Task.FromResult(claimResult);
    }
}