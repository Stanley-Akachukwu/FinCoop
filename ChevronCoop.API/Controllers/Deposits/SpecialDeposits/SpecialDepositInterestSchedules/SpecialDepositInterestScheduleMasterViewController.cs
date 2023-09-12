using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;

namespace ChevronCoop.API.Controllers.Deposits.SpecialDeposits
{




[ApiController]
    //[ApiVersion("1.0")]
	[ApiExplorerSettings(IgnoreApi = false,GroupName="Deposits")]
    [Route("[controller]")]
     [ODataAttributeRouting]

    public class SpecialDepositInterestScheduleMasterViewController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<SpecialDepositInterestScheduleMasterViewController> logger;
        private readonly IMapper mapper;
		private readonly ChevronCoopDbContext dbContext;
        public SpecialDepositInterestScheduleMasterViewController(IMediator _mediator,ChevronCoopDbContext appDb,
        ILogger<SpecialDepositInterestScheduleMasterViewController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
			dbContext = appDb;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
		[ProducesResponseType(typeof(ODataResponse<SpecialDepositInterestScheduleMasterView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
           return Ok(dbContext.SpecialDepositInterestScheduleMasterView);
        }


       

    }
	
		
}

