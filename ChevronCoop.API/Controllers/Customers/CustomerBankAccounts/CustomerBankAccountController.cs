using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts;
using AP.ChevronCoop.Commons;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChevronCoop.API.Config;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Customers.CustomerBankAccounts;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Customer")]
[Route("[controller]")]
[ODataAttributeRouting]
public class CustomerBankAccountController : ControllerBase //ODataController
{
  private readonly ILogger<CustomerBankAccountController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public CustomerBankAccountController(IMediator _mediator,
    ILogger<CustomerBankAccountController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<CustomerBankAccountViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    var request = new QueryCustomerBankAccountCommand();

    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    return result;
  }


  [EnableQuery]
  [HttpGet("({key})")]
  // [ODataRoute("({key})")]
  [ProducesResponseType(typeof(CustomerBankAccountViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<SingleResult<CustomerBankAccountViewModel>> Get(
    [FromODataUri] [Required] [CustomizeValidator(Skip = true)] string key)
  {
    var request = new QueryCustomerBankAccountCommand();
    var rsp = await mediator.Send(request);
    var queryable = rsp.Response.Where(p => p.Id == key);
    return SingleResult.Create(queryable.ProjectTo<CustomerBankAccountViewModel>(mapper.ConfigurationProvider));
  }

  [HttpPost("create")]
  [ProducesResponseType(typeof(CommandResult<CustomerBankAccountViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Create([FromBody] CreateCustomerBankAccountCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [HttpPost("update")]
  [ProducesResponseType(typeof(CommandResult<CustomerBankAccountViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Update([FromBody] UpdateCustomerBankAccountCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [HttpPost("delete")]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Delete([FromBody] DeleteCustomerBankAccountCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }
}