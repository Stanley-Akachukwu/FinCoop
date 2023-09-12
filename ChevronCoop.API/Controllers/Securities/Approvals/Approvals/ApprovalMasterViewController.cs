using AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChevronCoop.API.Controllers.Securities.Approvals;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Security")]
[Route("[controller]")]
[ODataAttributeRouting]

public class ApprovalMasterViewController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<ApprovalMasterViewController> logger;
    private readonly IMapper mapper;
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IApprovalDetailFactory _approvalDetail;
    private CancellationTokenSource cts = new CancellationTokenSource();
    private readonly IMemoryCache _memoryCache;
  private readonly ISDDailyInterestComputationService _computationService;
    private readonly ISDMonthlyInterestComputationService _mComputationService;
    public ApprovalMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb, ISDDailyInterestComputationService computationService, ISDMonthlyInterestComputationService mComputationService,
    ILogger<ApprovalMasterViewController> _logger, IMapper _mapper, IApprovalDetailFactory approvalDetail, IMemoryCache memoryCache)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        _dbContext = appDb;
        _approvalDetail = approvalDetail;
        _memoryCache= memoryCache;
        _computationService = computationService;
        _mComputationService= mComputationService;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<ApprovalMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
    //  await  _approvalDetail.ProcessDetail(ApprovalType.KYC_COMPLETION, "");
        return Ok(_dbContext.ApprovalMasterView);
    }

    [HttpGet("getById/{ApprovalId}")]
    [ProducesResponseType(typeof(ODataResponse<ApprovalMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string ApprovalId)
    {

        return Ok(_dbContext.ApprovalMasterView.Where(a=>a.Id== ApprovalId).ToList());
    }
    [EnableQuery]
    [HttpGet("userApprovals/{applicationUserId}")]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<ApprovalView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> QueryUserApproval(string applicationUserId)
    {
        var request = new QueryUserApprovalCommand(applicationUserId);
        var rsp = await mediator.Send(request);
        return Ok(rsp.Response);
    }


   
}






