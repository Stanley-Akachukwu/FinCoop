using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Commons;
using ChevronCoop.API.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.Auth.ApplicationRoleClaims;


[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class ApplicationRoleClaimController : ControllerBase //ODataController
{

    private readonly IMediator mediator;

    public ApplicationRoleClaimController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<ApplicationRoleClaimViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var request = new QueryApplicationRoleClaimCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    //[EnableQuery]
    //[HttpGet("({key})")]
    //// [ODataRoute("({key})")]
    //[ProducesResponseType(typeof(ApplicationRoleClaimViewModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<SingleResult<ApplicationRoleClaimViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    //{
    //    var request = new QueryApplicationRoleClaimCommand();
    //    var rsp = await mediator.Send(request);
    //    var queryable = rsp.Response.Where(p => p.Id == key);
    //    return SingleResult.Create(queryable.ProjectTo<ApplicationRoleClaimViewModel>(mapper.ConfigurationProvider));

    //}

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<ApplicationRoleClaimViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateApplicationRoleClaimCommand model)
    {

        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("update")]
    [ProducesResponseType(typeof(CommandResult<ApplicationRoleClaimViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateApplicationRoleClaimCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("delete")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteApplicationRoleClaimCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;

    }


}



