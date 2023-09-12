using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Payroll.PayrollCronJobConfigurations;
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
public class PayrollCronJobConfigMasterViewController : ControllerBase //ODataController
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<PayrollCronJobConfigMasterViewController> logger;
    private readonly IMapper mapper;

    private readonly IMediator mediator;

    public PayrollCronJobConfigMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
      ILogger<PayrollCronJobConfigMasterViewController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        dbContext = appDb;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<PayrollCronJobConfigMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(dbContext.PayrollCronJobConfigMasterView);
    }

    [EnableQuery]
    [HttpGet("{scheduleId}")]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<PayrollCronJobConfigMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBySchedulId([FromRoute] string scheduleId)
    {
        var query = await dbContext.PayrollCronJobConfigMasterView.Where(c => c.DeductionScheduleId == scheduleId).ToListAsync();

        return Ok(query);
    }

    /// <summary>
    /// Get Job by job Id
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    [EnableQuery]
    [HttpGet("job/{jobId}")]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<PayrollCronJobConfigMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetJobByJobId([FromRoute] string jobId)
    {
        var query = await dbContext.PayrollCronJobConfigMasterView.Where(c => c.Id == jobId).ToListAsync();

        return Ok(query);
    }
}