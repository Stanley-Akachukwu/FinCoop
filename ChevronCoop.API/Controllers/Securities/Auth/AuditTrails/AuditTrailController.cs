using AP.ChevronCoop.AppDomain.Security.AuditTrails;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Audit.Core;
using ChevronCoop.API.Config;
using ChevronCoop.API.CustomFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.Auth.AuditTrails;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]
public class AuditTrailController : ControllerBase
{
  private readonly IMediator mediator;

  public AuditTrailController(IMediator _mediator)
  {
    mediator = _mediator;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<AuditTrailViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    AuditScope.Log("AuditTrail:Get", new { ReferenceId = Guid.NewGuid() });
    var request = new QueryAuditTrailCommand();
    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    return result;
  }

	[HttpPost("log")]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> LogAudit(CreateAuditTrailCommand request)
	{
		var rsp = await mediator.Send(request);
		var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
		return result;
	}

	
}