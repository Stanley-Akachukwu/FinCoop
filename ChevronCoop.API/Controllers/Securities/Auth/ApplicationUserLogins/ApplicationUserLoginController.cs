using System.Net;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;
using ChevronCoop.API.Config;
using ChevronCoop.API.CustomFilters;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.Auth.ApplicationUserLogins;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class ApplicationUserLoginController : ControllerBase //ODataController
{

	private readonly IMediator mediator;
	private readonly IConfiguration _config;

	public ApplicationUserLoginController(IMediator _mediator,
		IConfiguration config
		)
	{
		mediator = _mediator;
		_config = config;
	}

	[EnableQuery]
	[HttpGet]
	//[ODataRoute]
	[ProducesResponseType(typeof(ODataResponse<ApplicationUserLoginViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get()
	{
		var request = new QueryApplicationUserLoginCommand();

		var rsp = await mediator.Send(request);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
		return result;
	}


	

	[HttpPost("login")]
	[ProducesResponseType(typeof(CommandResult<LoginViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Login(LoginCommand request)
	{
		var rsp = await mediator.Send(request);
		if (rsp.StatusCode == (int)HttpStatusCode.OK)
		{
			rsp.Response = rsp.Response.GenerateToken(_config);
		}
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}


	[HttpPost("create")]
	//[ServiceFilter(typeof(ChevronCoopLogInterceptor<CreateApplicationUserLoginCommand>))]
	[ProducesResponseType(typeof(CommandResult<ApplicationUserLoginViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Create([FromBody] CreateApplicationUserLoginCommand model)
	{
		//var currentUser = HttpContext.Items["CurrentUser"] as MemberProfile;

		var rsp = await mediator.Send(model);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}


	[HttpPost("update")]
	[ProducesResponseType(typeof(CommandResult<ApplicationUserLoginViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Update([FromBody] UpdateApplicationUserLoginCommand model)
	{
		var rsp = await mediator.Send(model);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}


	[HttpPost("delete")]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete([FromBody] DeleteApplicationUserLoginCommand model)
	{
		var rsp = await mediator.Send(model);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;

	}

	[HttpPost("forgetPassword")]
	[ProducesResponseType(typeof(CommandResult<ForgetPasswordViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> ForgetPassword(ForgetPasswordCommand request)
	{
		var rsp = await mediator.Send(request);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}

	[HttpPost("validateOTP")]
	[ProducesResponseType(typeof(CommandResult<ValidateForgetPasswordOTPViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> ValidateResetPasswordOTP(ValidateForgetPasswordOTPCommand request)
	{
		var rsp = await mediator.Send(request);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}

	[HttpPost("changePassword")]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> ChangePassword(ChangePasswordCommand request)
	{
		var rsp = await mediator.Send(request);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}

	[HttpPost("resetPassword")]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> ResetPassword(ResetPasswordCommand request)
	{
		var rsp = await mediator.Send(request);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}
	
	[HttpPost("switchAccount")]
	[ProducesResponseType(typeof(CommandResult<LoginViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> SwitchAccount(SwitchAccountCommand request)
	{
		var rsp = await mediator.Send(request);
		if ((int)rsp.StatusCode == (int)HttpStatusCode.OK)
		{
			rsp.Response = rsp.Response.GenerateToken(_config);
		}
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}

}



