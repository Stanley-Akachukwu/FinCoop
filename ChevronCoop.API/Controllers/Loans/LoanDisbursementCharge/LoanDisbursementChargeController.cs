using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Loans.LoanDisbursementCharges;
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

namespace ChevronCoop.API.Controllers.Loans.LoanDisbursementCharge;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Loans")]
[Route("[controller]")]
[ODataAttributeRouting]
public class LoanDisbursementChargeController : ControllerBase //ODataController
{
  private readonly ILogger<LoanDisbursementChargeController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public LoanDisbursementChargeController(IMediator _mediator,
    ILogger<LoanDisbursementChargeController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<LoanDisbursementChargeViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    var request = new QueryLoanDisbursementChargeCommand();

    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    return result;
  }


  [EnableQuery]
  [HttpGet("({key})")]
  // [ODataRoute("({key})")]
  [ProducesResponseType(typeof(LoanDisbursementChargeViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<SingleResult<LoanDisbursementChargeViewModel>> Get(
    [FromODataUri] [Required] [CustomizeValidator(Skip = true)] string key)
  {
    var request = new QueryLoanDisbursementChargeCommand();
    var rsp = await mediator.Send(request);
    var queryable = rsp.Response.Where(p => p.Id == key);
    return SingleResult.Create(queryable.ProjectTo<LoanDisbursementChargeViewModel>(mapper.ConfigurationProvider));
  }

  [HttpPost("create")]
  [ProducesResponseType(typeof(CommandResult<LoanDisbursementChargeViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Create([FromBody] CreateLoanDisbursementChargeCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [HttpPost("update")]
  [ProducesResponseType(typeof(CommandResult<LoanDisbursementChargeViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Update([FromBody] UpdateLoanDisbursementChargeCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [HttpPost("delete")]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Delete([FromBody] DeleteLoanDisbursementChargeCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }
}