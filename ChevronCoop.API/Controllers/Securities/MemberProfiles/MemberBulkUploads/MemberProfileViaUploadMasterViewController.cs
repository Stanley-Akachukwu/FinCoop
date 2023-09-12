using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.MemberProfiles.RetireeSwitches;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]
public class MemberProfileViaUploadMasterViewController : ControllerBase //ODataController
{
  private readonly ChevronCoopDbContext _dBcontext;
  private readonly ILogger<MemberProfileViaUploadMasterViewController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public MemberProfileViaUploadMasterViewController(IMediator _mediator, ChevronCoopDbContext dBcontext,
    ILogger<MemberProfileViaUploadMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    _dBcontext = dBcontext;
  }


  [EnableQuery]
  [HttpGet]
  [ProducesResponseType(typeof(ODataResponse<MemberProfileViaUploadMasterView>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    return Ok(_dBcontext.MemberProfileViaUploadMasterView);
  }
}