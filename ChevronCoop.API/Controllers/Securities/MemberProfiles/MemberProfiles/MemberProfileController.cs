using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChevronCoop.API.Config;
using ChevronCoop.API.CustomFilters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.MemberProfiles.MemberProfiles;

[ApiController]
//[ApiVersion("1.0")]
// [Authorize(AuthenticationSchemes = "OAuth")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class MemberProfileController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ILogger<MemberProfileController> logger;
    private readonly ChevronCoopDbContext _context;
    private readonly IConfiguration config;
    private readonly IMapper mapper;


    public MemberProfileController(IMediator _mediator,
        UserManager<ApplicationUser> _userManager,
        ILogger<MemberProfileController> _logger,
        ChevronCoopDbContext context,
        IConfiguration _config,
        IMapper _mapper)
    {
        mediator = _mediator;
        userManager = _userManager;
        logger = _logger;
        _context = context;
        config = _config;
        mapper = _mapper;
    }

    [EnableQuery]
    [HttpGet]
	// [AuthorizedPermission(PermissionConfig.SecurityMemberView)]
    [ProducesResponseType(typeof(ODataResponse<MemberProfileViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
		var request = new QueryMemberProfileCommand() {};
		var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    [EnableQuery]
    [HttpGet("({key})")]
    // [ODataRoute("({key})")]
    [ProducesResponseType(typeof(MemberProfileViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<SingleResult<MemberProfileViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    {
        var request = new QueryMemberProfileCommand();
        var rsp = await mediator.Send(request);
        var queryable = rsp.Response.Where(p => p.Id == key);
        return SingleResult.Create(queryable.ProjectTo<MemberProfileViewModel>(mapper.ConfigurationProvider));

    }


    [HttpPost("enrollment")]
    [ProducesResponseType(typeof(CommandResult<RegisterMemberViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(RegisterMemberCommand request)
    {
        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }
    [HttpPost("enrollment/update")]
    [ProducesResponseType(typeof(CommandResult<RegisterMemberViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(UpdateMemberRegistrationCommand request)
    {
        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }

    [HttpGet("verify")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Verify([FromQuery]string token, [FromQuery]string email)
    {
        var request = new VerifyMemberProfileCommand();
        request.Email = email;
        request.Token = token;
        
        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<MemberProfileViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateMemberProfileCommand model)
    {

        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }
    
    [HttpPost("approveKyc")]
    [ProducesResponseType(typeof(CommandResult<MemberProfileViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ApproveKyc([FromBody] ApproveKYCCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
        
    }


    [HttpPost("update")]
    [ProducesResponseType(typeof(CommandResult<MemberProfileViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateMemberProfileCommand model)
    {
		var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }
    
    
    [HttpPost("switchToRetiree")]
    [ProducesResponseType(typeof(CommandResult<MemberProfileViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SwitchToRetiree([FromBody] CreateRetireeSwitchCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("delete")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteMemberProfileCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


}



