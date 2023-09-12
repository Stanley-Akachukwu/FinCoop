using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using AP.ChevronCoop.Entities.Employees;

namespace ChevronCoop.API.Controllers.Employees
{
    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "HR")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class EmployeeMasterViewController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<EmployeeMasterViewController> logger;
        private readonly IMapper mapper;
        private readonly ChevronCoopDbContext dbContext;
        public EmployeeMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
        ILogger<EmployeeMasterViewController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
            dbContext = appDb;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<EmployeeMasterView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(dbContext.EmployeeMasterView);
        }




    }












}