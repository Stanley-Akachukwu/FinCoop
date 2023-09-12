using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Customers.Customers;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Customer")]
[Route("[controller]")]
[ODataAttributeRouting]
public class CustomerMasterViewController : ControllerBase //ODataController
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<CustomerMasterViewController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public CustomerMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<CustomerMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    dbContext = appDb;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<CustomerMasterView>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    return Ok(dbContext.CustomerMasterView);
  }
}