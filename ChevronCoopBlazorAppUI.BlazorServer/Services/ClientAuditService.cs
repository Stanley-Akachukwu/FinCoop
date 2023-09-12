using AP.ChevronCoop.AppDomain.Security.AuditTrails;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.AuditTrails;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
	public class ClientAuditService: IClientAuditService
	{
		private readonly IEntityDataService _dataService;
		private readonly IConfiguration _configuration;

		public ClientAuditService(IEntityDataService dataService, IConfiguration configuration)
        {
			_dataService= dataService;
			_configuration = configuration;
		}
		public async Task<CommandResult<string>> LogAudit(string action, string description, string module, string payload, ClaimsPrincipal CurrentUser)
		{
			var rsp = new CommandResult<string>();
			try
			{
				var memberShipId = CurrentUser.FindFirstValue("MemberShipId");
				var audit = new CreateAuditTrailCommand
				{
					Action = action,
					Description = description,
					Module = module,
					CreatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid),
					Payload = payload,
					UserName = CurrentUser.FindFirstValue(ClaimTypes.Name) + $"({memberShipId})"
				};
				var baseAddress = new Uri(_configuration[ConfigKeys.API_HOST]);
				var logRsponse = await _dataService.CreateAuditTrail<CreateAuditTrailCommand, CommandResult<string>>(audit);
				rsp = logRsponse.Content;
			}
			catch (Exception ex)
			{
				rsp = new CommandResult<string>() {
					Message = ex.Message,
					Response = ex.StackTrace,
					ErrorFlag = true,
					StatusCode = StatusCodes.Status500InternalServerError
				};
			}
			return await Task.FromResult(rsp);
		}

		public async Task<CommandResult<string>> LogAuditByEmail(string action, string description, string module, string payload, string CurrentUser)
		{
			var rsp = new CommandResult<string>();
			try
			{
				var audit = new CreateAuditTrailCommand
				{
					Action = action,
					Description = description,
					Module = module,
					CreatedByUserId = CurrentUser ,
					Payload = payload,
					UserName = CurrentUser
				};
				var baseAddress = new Uri(_configuration[ConfigKeys.API_HOST]);
				var logRsponse = await _dataService.CreateAuditTrail<CreateAuditTrailCommand, CommandResult<string>>(audit);
				rsp = logRsponse.Content;
			}
			catch (Exception ex)
			{
				rsp = new CommandResult<string>()
				{
					Message = ex.Message,
					Response = ex.StackTrace,
					ErrorFlag = true,
					StatusCode = StatusCodes.Status500InternalServerError
				};
			}
			return await Task.FromResult(rsp);
		}
	}
	//public async Task LogAuditForDownloadAction()
	//{
	//	var refitSettings = new RefitSettings()
	//	{
	//		AuthorizationHeaderValueGetter = () => Task.FromResult(bearToken)
	//	};
	//	//string action, string description, string module, string createdByUserId, string payload, string username)
	//	var memberShipId = CurrentUser.FindFirstValue("MemberShipId");


	//	var audit = new CreateAuditTrailCommand
	//	{
	//		Action = "User Bulk Download",
	//		Description = $"Executed bulk download of user list",
	//		Module = "Security",
	//		CreatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid),
	//		Payload = "NA, file download request",
	//		UserName = CurrentUser.FindFirstValue(ClaimTypes.Name) + $"({memberShipId})"
	//	};
	//	var baseAddress = new Uri(configuration[ConfigKeys.API_HOST]);
	//	var _dataServiceClient = RestService.For<IEntityDataService>(baseAddress.ToString(), refitSettings);
	//	var rsp = await _dataServiceClient.CreateAuditTrail<CreateAuditTrailCommand, CommandResult<string>>(audit);

	//}
}
