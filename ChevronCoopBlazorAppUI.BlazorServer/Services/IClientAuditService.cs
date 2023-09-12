using AP.ChevronCoop.Commons;
using System.Security.Claims;


namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
	public interface IClientAuditService
	{
		Task<CommandResult<string>> LogAudit(string action, string description, string module, string payload, ClaimsPrincipal CurrentUser);
		Task<CommandResult<string>> LogAuditByEmail(string action, string description, string module, string payload, string CurrentUser);

	}

}
