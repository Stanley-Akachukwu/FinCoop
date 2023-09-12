using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;

namespace ChevronCoop.API.Controllers.Accounting.JournalEntries
{
    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Accounting")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class JournalEntryMasterViewController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<JournalEntryMasterViewController> logger;
        private readonly IMapper mapper;
        private readonly ChevronCoopDbContext dbContext;
        public JournalEntryMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
        ILogger<JournalEntryMasterViewController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
            dbContext = appDb;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<JournalEntryMasterView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation($"query->{Request.QueryString}");
            return Ok(dbContext.JournalEntryMasterView);
        }




    }












}