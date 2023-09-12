using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Loans.LoanAccount;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Loans")]
[Route("[controller]")]
[ODataAttributeRouting]
public class LoanAccountMasterViewController : ControllerBase //ODataController
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<LoanAccountMasterViewController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public LoanAccountMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<LoanAccountMasterViewController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
    dbContext = appDb;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<LoanAccountMasterView>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    return Ok(dbContext.LoanAccountMasterView);
  }
}