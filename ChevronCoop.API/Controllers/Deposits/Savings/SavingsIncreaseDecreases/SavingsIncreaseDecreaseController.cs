using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
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
using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.API.Controllers.Deposits.Savings.SavingIncreaseDecreases;


[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Deposits")]
[Route("[controller]")]
[ODataAttributeRouting]

public class SavingsIncreaseDecreaseController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<SavingsIncreaseDecreaseController> logger;
    private readonly IMapper mapper;

    public SavingsIncreaseDecreaseController(IMediator _mediator,
    ILogger<SavingsIncreaseDecreaseController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<SavingsIncreaseDecreaseViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var request = new QuerySavingsIncreaseDecreaseCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    [EnableQuery]
    [HttpGet("({key})")]
    // [ODataRoute("({key})")]
    [ProducesResponseType(typeof(SavingsIncreaseDecreaseViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<SingleResult<SavingsIncreaseDecreaseViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    {
        var request = new QuerySavingsIncreaseDecreaseCommand();
        var rsp = await mediator.Send(request);
        var queryable = rsp.Response.Where(p => p.Id == key);
        return SingleResult.Create(queryable.ProjectTo<SavingsIncreaseDecreaseViewModel>(mapper.ConfigurationProvider));

    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<SavingsIncreaseDecreaseViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateSavingsIncreaseDecreaseCommand model)
    {

        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    //[HttpPost("update")]
    //[ProducesResponseType(typeof(CommandResult<SavingsIncreaseDecreaseViewModel>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Update([FromBody] UpdateSavingsIncreaseDecreaseCommand model)
    //{
    //    var rsp = await mediator.Send(model);
    //    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    //    return result;
    //}


    //[HttpPost("delete")]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Delete([FromBody] DeleteSavingsIncreaseDecreaseCommand model)
    //{
    //    var rsp = await mediator.Send(model);
    //    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    //    return result;

    //}


}



