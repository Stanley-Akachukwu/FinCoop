
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
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

namespace ChevronCoop.API.Controllers.Deposits.Savings.SavingsAccounts;


[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Deposits")]
[Route("[controller]")]
[ODataAttributeRouting]

public class SavingsAccountController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<SavingsAccountController> logger;
    private readonly IMapper mapper;

    public SavingsAccountController(IMediator _mediator,
    ILogger<SavingsAccountController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<SavingsAccountViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var request = new QuerySavingsAccountCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    [EnableQuery]
    [HttpGet("({key})")]
    // [ODataRoute("({key})")]
    [ProducesResponseType(typeof(SavingsAccountViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<SingleResult<SavingsAccountViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    {
        var request = new QuerySavingsAccountCommand();
        var rsp = await mediator.Send(request);
        var queryable = rsp.Response.Where(p => p.Id == key);
        return SingleResult.Create(queryable.ProjectTo<SavingsAccountViewModel>(mapper.ConfigurationProvider));

    }

    //[HttpPost("create")]
    //[ProducesResponseType(typeof(CommandResult<SavingsAccountViewModel>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Create([FromBody] CreateSavingsAccountCommand model)
    //{

    //    var rsp = await mediator.Send(model);
    //    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    //    return result;
    //}


    //[HttpPost("update")]
    //[ProducesResponseType(typeof(CommandResult<SavingsAccountViewModel>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Update([FromBody] UpdateSavingsAccountCommand model)
    //{
    //    var rsp = await mediator.Send(model);
    //    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    //    return result;
    //}


    //[HttpPost("delete")]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Delete([FromBody] DeleteSavingsAccountCommand model)
    //{
    //    var rsp = await mediator.Send(model);
    //    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    //    return result;

    //}


}



