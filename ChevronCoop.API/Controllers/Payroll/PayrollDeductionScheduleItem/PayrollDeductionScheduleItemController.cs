using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppCore.Services.BackgroundServices;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionScheduleItems;
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
public class PayrollDeductionScheduleItemController : ControllerBase //ODataController
{
    private readonly ILogger<PayrollDeductionScheduleItemController> logger;
    private readonly IMapper mapper;

    private readonly IMediator mediator;
    private readonly IPayrollScheduleBackgroundService _payrollScheduleBackgroundService;

    public PayrollDeductionScheduleItemController(IMediator _mediator,
    ILogger<PayrollDeductionScheduleItemController> _logger, IMapper _mapper,
    IPayrollScheduleBackgroundService payrollScheduleBackgroundService)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        _payrollScheduleBackgroundService = payrollScheduleBackgroundService;

    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<PayrollDeductionScheduleItemViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var request = new QueryPayrollDeductionScheduleItemCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    [EnableQuery]
    [HttpGet("({key})")]
    // [ODataRoute("({key})")]
    [ProducesResponseType(typeof(PayrollDeductionScheduleItemViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<SingleResult<PayrollDeductionScheduleItemViewModel>> Get(
      [FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    {
        var request = new QueryPayrollDeductionScheduleItemCommand();
        var rsp = await mediator.Send(request);
        var queryable = rsp.Response.Where(p => p.Id == key);
        return SingleResult.Create(
          queryable.ProjectTo<PayrollDeductionScheduleItemViewModel>(mapper.ConfigurationProvider));
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<PayrollDeductionScheduleItemViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreatePayrollDeductionScheduleItemCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("update")]
    [ProducesResponseType(typeof(CommandResult<PayrollDeductionScheduleItemViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdatePayrollDeductionScheduleItemCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("delete")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeletePayrollDeductionScheduleItemCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }

    [HttpGet("runJonb/{jobId}")]
    [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RunJob([FromRoute] string jobId)
    {
        if (jobId == "1")
        {
            await _payrollScheduleBackgroundService.GetSavingDepositDeductions();
        }
        else if (jobId == "2")
        {
            await _payrollScheduleBackgroundService.GetSpecialDepositDeductions();
        }
        else if (jobId == "3")
        {
            await _payrollScheduleBackgroundService.GetLoanRepaymentDeductions();
        }
        else if (jobId == "4")
        {
            await _payrollScheduleBackgroundService.MatchDeductionAndPayrollData("01H7AKD9HTQM200MMVRDJX8W54");
        }
       

        return Ok(new { Status = "00", Message = "Success" });
    }

}