using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
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

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Payroll")]
[Route("[controller]")]
[ODataAttributeRouting]
public class PayrollCronJobConfigController : ControllerBase //ODataController
{
    private readonly ILogger<PayrollCronJobConfigController> logger;
    private readonly IMapper mapper;

    private readonly IMediator mediator;

    public PayrollCronJobConfigController(IMediator _mediator,
      ILogger<PayrollCronJobConfigController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
    }

    //[EnableQuery]
    //[HttpGet]
    ////[ODataRoute]
    //[ProducesResponseType(typeof(ODataResponse<PayrollCronJobConfigViewModel>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Get()
    //{
    //    var request = new QueryPayrollCronJobConfigCommand();

    //    var rsp = await mediator.Send(request);
    //    var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    //    return result;
    //}

    // chargeType from deposit product charge 
    [EnableQuery]
    [HttpGet("({key})")]
    // [ODataRoute("({key})")]
    [ProducesResponseType(typeof(PayrollCronJobConfigViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<SingleResult<PayrollCronJobConfigViewModel>> Get(
      [FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    {
        var request = new QueryPayrollCronJobConfigCommand();
        var rsp = await mediator.Send(request);
        var queryable = rsp.Response.Where(p => p.Id == key);
        return SingleResult.Create(queryable.ProjectTo<PayrollCronJobConfigViewModel>(mapper.ConfigurationProvider));
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<PayrollCronJobConfigViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreatePayrollCronJobConfigCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("update")]
    [ProducesResponseType(typeof(CommandResult<PayrollCronJobConfigViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdatePayrollCronJobConfigCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("delete")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeletePayrollCronJobConfigCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }
}