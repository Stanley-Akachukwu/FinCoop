using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
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
public class RetireeSwitchMasterViewController : ControllerBase //ODataController
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<RetireeSwitchMasterViewController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public RetireeSwitchMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<RetireeSwitchMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    dbContext = appDb;
  }

  // [EnableQuery]
  // [HttpGet]
  // //[ODataRoute]
  // [ProducesResponseType(typeof(ODataResponse<RetireeSwitchMasterView>), StatusCodes.Status200OK)]
  // [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  // [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  // public async Task<IActionResult> Get()
  // {
  //   return Ok(dbContext.RetireeSwitchMasterView);
  // }
}