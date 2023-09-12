using AP.ChevronCoop.AppDomain.Employees;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.RetireeSwitches;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AutoMapper;
using ChevronCoop.API.Config;
using ChevronCoop.API.Controllers.Securities.MemberProfiles.MemberProfiles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.MemberProfiles.RetireeSwitches;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class RetireeSwitchesController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ILogger<MemberProfileController> logger;
    private readonly ChevronCoopDbContext _context;
    private readonly IConfiguration config;
    private readonly IMapper mapper;


    public RetireeSwitchesController(IMediator _mediator,
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

     

    // [EnableQuery]
    // [HttpGet("GetAll")]
    // [ProducesResponseType(typeof(ODataResponse<RetireeSwitchViewModel>), StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    // public async Task<IActionResult> GetRetireeSwitches()
    // {
    //     var request = new QueryRetireeSwitchCommand();
    //
    //     var rsp = await mediator.Send(request);
    //     var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    //     return result;
    // }

    [HttpPost("switch")]
    [ProducesResponseType(typeof(CommandResult<EmployeeViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SwitchToRetiree([FromBody] CreateRetireeSwitchCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }

    [HttpPost("approve")]
    [ProducesResponseType(typeof(CommandResult<RetireeSwitchViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ApproveRetireeSwitch([FromBody] ApproveRetireeSwitchCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }

}



