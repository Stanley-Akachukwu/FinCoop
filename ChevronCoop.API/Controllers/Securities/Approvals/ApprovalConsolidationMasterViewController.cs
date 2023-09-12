// using AP.ChevronCoop.Entities;
// using AutoMapper;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.OData.Routing.Attributes;
//
// namespace ChevronCoop.API.Controllers.Securities.Approvals;
//
// [ApiController]
// //[ApiVersion("1.0")]
// [ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
// [Route("[controller]")]
// [ODataAttributeRouting]
// public class ApprovalConsolidationMasterViewController : ControllerBase //ODataController
// {
//   private readonly ChevronCoopDbContext _dbContext;
//   private readonly ILogger<ApprovalConsolidationMasterViewController> logger;
//   private readonly IMapper mapper;
//
//   private readonly IMediator mediator;
//
//   public ApprovalConsolidationMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
//     ILogger<ApprovalConsolidationMasterViewController> _logger, IMapper _mapper)
//   {
//     mediator = _mediator;
//     logger = _logger;
//     mapper = _mapper;
//     _dbContext = appDb;
//   }
//
//   // [EnableQuery]
//   // [HttpGet]
//   // [ProducesResponseType(typeof(ODataResponse<ApprovalConsolidationMasterView>), StatusCodes.Status200OK)]
//   // [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
//   // [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
//   // public async Task<IActionResult> Get()
//   // {
//   //     return Ok(_dbContext.ApprovalConsolidationMasterView);
//   // }
// }