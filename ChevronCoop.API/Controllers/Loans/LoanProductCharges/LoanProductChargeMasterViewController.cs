using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using AP.ChevronCoop.Entities.Loans.LoanProductCharges;

namespace ChevronCoop.API.Controllers.Loans.LoanProductCharges
{
    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Loans")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class LoanProductChargeMasterViewController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<LoanProductChargeMasterViewController> logger;
        private readonly IMapper mapper;
        private readonly ChevronCoopDbContext dbContext;
        public LoanProductChargeMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
        ILogger<LoanProductChargeMasterViewController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
            dbContext = appDb;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<LoanProductChargeMasterView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(dbContext.LoanProductChargeMasterView);
        }




    }












}