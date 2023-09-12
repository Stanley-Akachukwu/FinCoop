using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Documents.OfficePhotos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Documents.OfficePhotos;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Docs")]
[Route("[controller]")]
[ODataAttributeRouting]

public class OfficePhotoMasterViewController : ControllerBase //ODataController
{

  private readonly IMediator mediator;
  private readonly ILogger<OfficePhotoMasterViewController> logger;
  private readonly IMapper mapper;
  private readonly ChevronCoopDbContext dbContext;
  public OfficePhotoMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<OfficePhotoMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    dbContext = appDb;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<OfficePhotoMasterView>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    return Ok(dbContext.OfficePhotoMasterView);
  }




}
