using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.Entities.Payroll;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.AppCore.Services.BackgroundServices;
using ChevronCoop.API.Config;

namespace ChevronCoop.API.Controllers.Deposits.DepositProducts
{
    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Payroll")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class PayrollDeductionItemMasterViewController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<PayrollDeductionItemMasterViewController> logger;
        private readonly IMapper mapper;
        private readonly ChevronCoopDbContext dbContext;

        public PayrollDeductionItemMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
        ILogger<PayrollDeductionItemMasterViewController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
            dbContext = appDb;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<PayrollDeductionItemMasterView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            //var dd = dbContext.PayrollDeductionItemMasterView;
            return Ok(dbContext.PayrollDeductionItemMasterView);
        }

        /// <summary>
        /// Payroll deduction Schedule
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        [EnableQuery]
        [HttpGet("{scheduleId}")]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<PayrollDeductionItemMasterView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDeductionSchedulById([FromRoute] string scheduleId)
        {
            var query = await dbContext.PayrollDeductionItemMasterView.Where(c => c.PayrollDeductionScheduleId == scheduleId).ToListAsync();

            return Ok(query);
        }





    }
}


