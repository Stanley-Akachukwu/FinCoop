using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.MasterData.Banks;
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
using Newtonsoft.Json;

namespace ChevronCoop.API.Controllers.Masterdata.Banks;


[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "MasterData")]
[Route("[controller]")]
[ODataAttributeRouting]

public class BankController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILoggerService _logger;
    private readonly IMapper mapper;


    private static string _className = "BankController";

    public BankController(IMediator _mediator, ILoggerService logger, IMapper _mapper)
    {
        mediator = _mediator;
        _logger = logger;
        mapper = _mapper;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<BankViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var methodName = "Get";
        _logger.LogInfo(_className, methodName, " Test loggings \r\n");
        var request = new QueryBankCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    [EnableQuery]
    [HttpGet("({key})")]
    // [ODataRoute("({key})")]
    [ProducesResponseType(typeof(BankViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<SingleResult<BankViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    {
        var request = new QueryBankCommand();
        var rsp = await mediator.Send(request);
        var queryable = rsp.Response.Where(p => p.Id == key);
        return SingleResult.Create(queryable.ProjectTo<BankViewModel>(mapper.ConfigurationProvider));

    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<BankViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateBankCommand model)
    {
        var methodName = "Create";
        _logger.LogInfo(_className, methodName, " CreateBankCommand Request Details \r\n" + JsonConvert.SerializeObject(model));
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("update")]
    [ProducesResponseType(typeof(CommandResult<BankViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateBankCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("delete")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteBankCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;

    }


}

