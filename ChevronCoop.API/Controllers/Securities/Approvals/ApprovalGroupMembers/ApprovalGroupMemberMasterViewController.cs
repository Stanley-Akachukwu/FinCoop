using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;

namespace ChevronCoop.API.Controllers.Securities.Approvals;

[ApiController]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class ApprovalGroupMemberMasterViewController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<ApprovalGroupMemberMasterViewController> logger;
    private readonly IMapper mapper;
    private readonly ChevronCoopDbContext _dbContext;
    public ApprovalGroupMemberMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,ILogger<ApprovalGroupMemberMasterViewController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        _dbContext = appDb;
    }

    [EnableQuery]
    [HttpGet]
    [ProducesResponseType(typeof(ODataResponse<ApprovalGroupMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(_dbContext.ApprovalGroupMemberMasterView);
    }
    //[EnableQuery]
    //[HttpGet]
    //[ProducesResponseType(typeof(ODataResponse<ApprovalGroupMasterView>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetByGroupId(string groupId)
    //{
    //    return Ok(await _dbContext.ApprovalGroupMembers.Where(g => g.ApprovalGroupId == groupId).ToListAsync());
    //}
}






