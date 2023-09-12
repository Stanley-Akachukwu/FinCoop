using System.Net;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AutoMapper;
using ChevronCoop.API.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.Approvals.ApprovalNotifications
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class ApprovalNotificationsController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly ILogger<ApprovalController> logger;
        private readonly IMapper mapper;

        public ApprovalNotificationsController(IMediator _mediator,
        ILogger<ApprovalController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
        }


        [HttpPost("create")]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateApprovalNotificationCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
        
        
        [HttpPost("send")]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SendApprovalRequestNotificationCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, (int)HttpStatusCode.OK);
            return result;
        }
    }
}
