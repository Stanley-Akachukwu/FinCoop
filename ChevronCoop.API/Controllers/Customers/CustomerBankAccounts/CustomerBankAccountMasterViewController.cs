using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Customers.CustomerBankAccounts;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Customer")]
[Route("[controller]")]
[ODataAttributeRouting]
public class CustomerBankAccountMasterViewController : ControllerBase //ODataController
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<CustomerBankAccountMasterViewController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public CustomerBankAccountMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<CustomerBankAccountMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    dbContext = appDb;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<CustomerBankAccountMasterView>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    return Ok(dbContext.CustomerBankAccountMasterView);
  }
}