using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;
using ChevronCoop.API.Config;
using ChevronCoop.API.CustomFilters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.MemberProfiles.MemberProfiles;

[ApiController]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]
public class MemberProfileMasterViewController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<MemberProfileMasterViewController> logger;
    private readonly IMapper mapper;
    private readonly ChevronCoopDbContext dbContext;
    public MemberProfileMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<MemberProfileMasterViewController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        dbContext = appDb;
    }

    [EnableQuery]
    [HttpGet]
	[ProducesResponseType(typeof(ODataResponse<MemberProfileMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(dbContext.MemberProfileMasterView);
    }




}






