using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberNextOfKins;
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

namespace ChevronCoop.API.Controllers.Securities.MemberProfiles.MemberNextOfKins;


[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class MemberNextOfKinController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<MemberNextOfKinController> logger;
    private readonly IMapper mapper;

    public MemberNextOfKinController(IMediator _mediator,
    ILogger<MemberNextOfKinController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<MemberNextOfKinViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var request = new QueryMemberNextOfKinCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    [EnableQuery]
    [HttpGet("({key})")]
    // [ODataRoute("({key})")]
    [ProducesResponseType(typeof(MemberNextOfKinViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<SingleResult<MemberNextOfKinViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
    {
        var request = new QueryMemberNextOfKinCommand();
        var rsp = await mediator.Send(request);
        var queryable = rsp.Response.Where(p => p.Id == key);
        return SingleResult.Create(queryable.ProjectTo<MemberNextOfKinViewModel>(mapper.ConfigurationProvider));

    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<MemberNextOfKinViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateMemberNextOfKinCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("update")]
    [ProducesResponseType(typeof(CommandResult<MemberNextOfKinViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateMemberNextOfKinCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("delete")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteMemberNextOfKinCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;

    }


}



