using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Commons;
using AutoMapper;
using ChevronCoop.API.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.Approvals;

[ApiController]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]
public class ApprovalGroupController : ControllerBase //ODataController 
{
	private readonly ILogger<ApprovalGroupController> logger;
	private readonly IMapper mapper;

	private readonly IMediator mediator;

	public ApprovalGroupController(IMediator _mediator,
		ILogger<ApprovalGroupController> _logger, IMapper _mapper)
	{
		mediator = _mediator;
		logger = _logger;
		mapper = _mapper;
	}
	
	
	[EnableQuery]
	[HttpGet]
	//[ODataRoute]
	[ProducesResponseType(typeof(ODataResponse<ApprovalGroupViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get()
	{
		var request = new QueryApprovalGroupCommand();

		var rsp = await mediator.Send(request);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
		return result;
	}


	// [EnableQuery]
	// [ODataRoute("({key})")]
	[HttpGet("GetApprovalGroupById/{id}")]
	[ProducesResponseType(typeof(GetApprovalGroupViewModel), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetApprovalGroupById([FromRoute] string id)
	{
		var request = new GetApprovalGroupCommand(id);
		var rsp = await mediator.Send(request);
		return await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);

	}
	

	[HttpPost("create")]
	[ProducesResponseType(typeof(CommandResult<ApprovalGroupViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateGroup([FromBody] CreateApprovalGroupCommand model)
	{
		var rsp = await mediator.Send(model);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}


	[HttpPost("update")]
	[ProducesResponseType(typeof(CommandResult<ApprovalGroupViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateGroup([FromBody] UpdateApprovalGroupCommand model)
	{
		var rsp = await mediator.Send(model);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}
 

	[HttpPost("delete")]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete([FromBody] DeleteApprovalGroupMemberCommand model)
	{
		var rsp = await mediator.Send(model);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}

	[HttpPost("createOrUpdateGroupMember")]
	[ProducesResponseType(typeof(CommandResult<ApprovalGroupViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateOrUpdateGroupMember([FromBody] CreateOrUpdateGroupMemberCommand model)
	{
		var rsp = await mediator.Send(model);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}

	//[HttpPost("fetchAll")]
	//[ProducesResponseType(typeof(CommandResult<List<ApprovalGroupViewModel>>), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	//[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	//public async Task<IActionResult> FetchAllApprovalGroups([FromBody] FetchAllApprovalGroupsCommand model)
	//{
	//	var rsp = await mediator.Send(model);
	//	var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
	//	return result;
	//}

	//[HttpPost("groupMembersBySessionId")]
	//[ProducesResponseType(typeof(CommandResult<List<ApprovalGroupMember>>), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	//[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	//public async Task<IActionResult> GetGroupMembersBySessionId(
	//	[FromBody] FetchApprovalGroupMembersBySessionIdCommand model)
	//{
	//	var rsp = await mediator.Send(model);
	//	var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
	//	return result;
	//}

	//[HttpPost("updateAll")]
	//[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	//[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	//public async Task<IActionResult> UpdateAllGroup([FromBody] UpdateAllApprovalGroupCommand model)
	//{
	//	var rsp = await mediator.Send(model);
	//	var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
	//	return result;
	//}
}