using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionScheduleItems;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Payroll")]
[Route("[controller]")]
[ODataAttributeRouting]
public class PayrollDeductionScheduleItemMasterViewController : ControllerBase //ODataController
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<PayrollDeductionScheduleItemMasterViewController> logger;
    private readonly IMapper mapper;

    private readonly IMediator mediator;

    public PayrollDeductionScheduleItemMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
      ILogger<PayrollDeductionScheduleItemMasterViewController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        dbContext = appDb;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<PayrollDeductionScheduleItemMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(dbContext.PayrollDeductionScheduleItemMasterView);
    }

    [HttpGet("matchPayroll/{scheduleId}")]
    [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMatchPayrollDeduction([FromRoute] string scheduleId)
    {
        var query = await dbContext.PayrollDeductionScheduleItemMasterView
                         .Where(c => c.PayrollDeductionScheduleId == scheduleId).ToListAsync();

        return Ok(query);
    }
}