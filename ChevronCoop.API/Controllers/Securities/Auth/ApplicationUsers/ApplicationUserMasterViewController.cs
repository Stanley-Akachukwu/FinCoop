using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Securities.Auth.ApplicationUsers;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class ApplicationUserMasterViewController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<ApplicationUserMasterViewController> logger;
    private readonly IMapper mapper;
    private readonly ChevronCoopDbContext dbContext;
    public ApplicationUserMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<ApplicationUserMasterViewController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        dbContext = appDb;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<ApplicationUserMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(dbContext.ApplicationUserMasterView);
    }




}






