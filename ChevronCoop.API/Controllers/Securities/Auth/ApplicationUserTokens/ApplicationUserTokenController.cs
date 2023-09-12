using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserTokens;
using AP.ChevronCoop.Commons;
using AutoMapper;
using ChevronCoop.API.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.Auth.ApplicationUserTokens;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class ApplicationUserTokenController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<ApplicationUserTokenController> logger;
    private readonly IMapper mapper;

    public ApplicationUserTokenController(IMediator _mediator,
    ILogger<ApplicationUserTokenController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<ApplicationUserTokenViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var request = new QueryApplicationUserTokenCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    //[EnableQuery]
    //[HttpGet("({key})")]
    //// [ODataRoute("({key})")]
    //[ProducesResponseType(typeof(ApplicationUserTokenViewModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<SingleResult<ApplicationUserTokenViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    //{
    //    var request = new QueryApplicationUserTokenCommand();
    //    var rsp = await mediator.Send(request);
    //    var queryable = rsp.Response.Where(p => p.Id == key);
    //    return SingleResult.Create(queryable.ProjectTo<ApplicationUserTokenViewModel>(mapper.ConfigurationProvider));

    //}

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<ApplicationUserTokenViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateApplicationUserTokenCommand model)
    {

        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("update")]
    [ProducesResponseType(typeof(CommandResult<ApplicationUserTokenViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateApplicationUserTokenCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("delete")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteApplicationUserTokenCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;

    }


}



