using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
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
public class MemberBulkUploadSessionMasterViewController : ControllerBase //ODataController
{
  private readonly ChevronCoopDbContext _dBcontext;
  private readonly ILogger<MemberBulkUploadSessionMasterViewController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public MemberBulkUploadSessionMasterViewController(IMediator _mediator, ChevronCoopDbContext dBcontext,
    ILogger<MemberBulkUploadSessionMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    _dBcontext = dBcontext;
  }


  [EnableQuery]
  [HttpGet]
  [ProducesResponseType(typeof(ODataResponse<MemberBulkUploadSessionMasterView>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    return Ok(_dBcontext.MemberBulkUploadSessionMasterView);
  }
}