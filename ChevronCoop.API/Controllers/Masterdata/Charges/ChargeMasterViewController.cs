using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Masterdata.Charges;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "MasterData")]
[Route("[controller]")]
[ODataAttributeRouting]
public class ChargeMasterViewController : ControllerBase //ODataController
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<ChargeMasterViewController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public ChargeMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<ChargeMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    dbContext = appDb;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<ChargeMasterView>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    return Ok(dbContext.ChargeMasterView);
  }
}