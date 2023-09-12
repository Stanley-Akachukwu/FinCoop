
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Commons;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChevronCoop.API.Config;
using FluentValidation.AspNetCore;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using System.ComponentModel.DataAnnotations;


namespace ChevronCoop.API.Controllers.Deposits.FixedDeposits.FixedDepositAccounts;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Deposits")]
[Route("[controller]")]
[ODataAttributeRouting]

public class FixedDepositAccountController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<FixedDepositAccountController> logger;
    private readonly IMapper mapper;
    private readonly IFixedDepositInterestComputationService _fixedDepositService;

    public FixedDepositAccountController(IMediator _mediator,
    ILogger<FixedDepositAccountController> _logger, IMapper _mapper , IFixedDepositInterestComputationService fixedDepositService)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        _fixedDepositService = fixedDepositService;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<FixedDepositAccountViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var request = new QueryFixedDepositAccountCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    [EnableQuery]
    [HttpGet("({key})")]
    // [ODataRoute("({key})")]
    [ProducesResponseType(typeof(FixedDepositAccountViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<SingleResult<FixedDepositAccountViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    {
        var request = new QueryFixedDepositAccountCommand();
        var rsp = await mediator.Send(request);
        var queryable = rsp.Response.Where(p => p.Id == key);
        return SingleResult.Create(queryable.ProjectTo<FixedDepositAccountViewModel>(mapper.ConfigurationProvider));

    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<FixedDepositAccountViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateFixedDepositAccountCommand model)
    {

        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpGet("RunInterestComputation")]
    [ProducesResponseType(typeof(CommandResult<FixedDepositAccountViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RunInterestComputation()
    {

        await _fixedDepositService.ProcessInterestComputation();
        return Ok();
    }



    [HttpGet("RunPostTransactiontest/{fixedDepositId}")]
    [ProducesResponseType(typeof(CommandResult<FixedDepositAccountViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RunInterestComputation([FromRoute] string fixedDepositId)
    {

        await _fixedDepositService.RepostFundingTransaction(fixedDepositId);
        return Ok();
    }


    //[HttpPost("update")]
    //[ProducesResponseType(typeof(CommandResult<FixedDepositAccountViewModel>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Update([FromBody] UpdateFixedDepositAccountCommand model)
    //{
    //    var rsp = await mediator.Send(model);
    //    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    //    return result;
    //}


    //[HttpPost("delete")]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Delete([FromBody] DeleteFixedDepositAccountCommand model)
    //{
    //    var rsp = await mediator.Send(model);
    //    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    //    return result;

    //}


}



