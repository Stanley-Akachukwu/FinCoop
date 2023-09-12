using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AutoMapper;
using ChevronCoop.API.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.Approvals;

[ApiController]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]
public class ApprovalGroupMemberController : ControllerBase  
{
	private readonly ILogger<ApprovalGroupMemberController> logger;
	private readonly IMapper mapper;
	private readonly IMediator mediator;

	public ApprovalGroupMemberController(IMediator _mediator,
		ILogger<ApprovalGroupMemberController> _logger, IMapper _mapper)
	{
		mediator = _mediator;
		logger = _logger;
		mapper = _mapper;
	}
	
	 

	[HttpPost("create")]
	[ProducesResponseType(typeof(CommandResult<ApprovalGroupMemberViewModel>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateGroupMember([FromBody] CreateApprovalGroupMemberCommand model)
	{
		var rsp = await mediator.Send(model);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}
}