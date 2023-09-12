using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Documents.DocumentTypes;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Documents.DocumentTypes;


[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Docs")]
[Route("[controller]")]
[ODataAttributeRouting]

public class DocumentTypeMasterViewController : ControllerBase //ODataController
{

  private readonly IMediator mediator;
  private readonly ILogger<DocumentTypeMasterViewController> logger;
  private readonly IMapper mapper;
  private readonly ChevronCoopDbContext dbContext;
  public DocumentTypeMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<DocumentTypeMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    dbContext = appDb;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<DocumentTypeMasterView>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    return Ok(dbContext.DocumentTypeMasterView);
  }




}

