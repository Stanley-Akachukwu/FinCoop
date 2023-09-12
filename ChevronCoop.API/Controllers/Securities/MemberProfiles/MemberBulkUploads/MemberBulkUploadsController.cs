using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using AutoMapper;
using ChevronCoop.API.Config;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.MemberProfiles.MemberProfiles;

[ApiController]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]
public class MemberBulkUploadsController : ControllerBase //ODataController
{
  private readonly ChevronCoopDbContext _dBcontext;
  private readonly IConfiguration config;
  private readonly ILogger<MemberProfileController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;
  private readonly UserManager<ApplicationUser> userManager;


  public MemberBulkUploadsController(IMediator _mediator,
    UserManager<ApplicationUser> _userManager,
    ILogger<MemberProfileController> _logger,
    ChevronCoopDbContext context,
    IConfiguration _config,
    IMapper _mapper)
  {
    mediator = _mediator;
    userManager = _userManager;
    logger = _logger;
    _dBcontext = context;
    config = _config;
    mapper = _mapper;
  }


  [HttpPost("validate")]
  [ProducesResponseType(typeof(CommandResult<MemberBulkUploadViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ValidateBulkploadMembers(ValidateMemberBulkUploadCommand request)
  {
    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }

  [HttpPost("save")]
  [ProducesResponseType(typeof(CommandResult<MemberBulkUploadViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> BulkploadMembers(CreateMemberBulkUploadCommand request)
  {
    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }

  [HttpPost("approve")]
  [ProducesResponseType(typeof(CommandResult<MemberBulkUploadViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ApproveMemberBulkUpload([FromBody] ApproveMemberBulkUploadCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }

  [HttpPost("getValidTempBulkUpload")]
  [ProducesResponseType(typeof(CommandResult<List<MemberBulkUploadTemp>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetMemberBulkUploadTemp([FromBody] GetMemberBulkUploadTempCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }
}