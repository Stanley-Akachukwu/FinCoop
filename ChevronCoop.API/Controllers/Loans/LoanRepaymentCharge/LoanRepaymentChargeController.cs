using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Loans.LoanRepaymentCharges;
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

namespace ChevronCoop.API.Controllers.Loans.LoanRepaymentCharge;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Loans")]
[Route("[controller]")]
[ODataAttributeRouting]
public class LoanRepaymentChargeController : ControllerBase //ODataController
{
  private readonly ILogger<LoanRepaymentChargeController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public LoanRepaymentChargeController(IMediator _mediator,
    ILogger<LoanRepaymentChargeController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<LoanRepaymentChargeViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    var request = new QueryLoanRepaymentChargeCommand();

    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    return result;
  }


  [EnableQuery]
  [HttpGet("({key})")]
  // [ODataRoute("({key})")]
  [ProducesResponseType(typeof(LoanRepaymentChargeViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<SingleResult<LoanRepaymentChargeViewModel>> Get(
    [FromODataUri] [Required] [CustomizeValidator(Skip = true)] string key)
  {
    var request = new QueryLoanRepaymentChargeCommand();
    var rsp = await mediator.Send(request);
    var queryable = rsp.Response.Where(p => p.Id == key);
    return SingleResult.Create(queryable.ProjectTo<LoanRepaymentChargeViewModel>(mapper.ConfigurationProvider));
  }

  [HttpPost("create")]
  [ProducesResponseType(typeof(CommandResult<LoanRepaymentChargeViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Create([FromBody] CreateLoanRepaymentChargeCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [HttpPost("update")]
  [ProducesResponseType(typeof(CommandResult<LoanRepaymentChargeViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Update([FromBody] UpdateLoanRepaymentChargeCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [HttpPost("delete")]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Delete([FromBody] DeleteLoanRepaymentChargeCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }
}