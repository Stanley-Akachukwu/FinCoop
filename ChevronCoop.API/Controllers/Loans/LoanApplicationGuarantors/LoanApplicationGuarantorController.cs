using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Commons;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChevronCoop.API.Config;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Loans.LoanApplicationGuarantors;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Loans")]
[Route("[controller]")]
[ODataAttributeRouting]
public class LoanApplicationGuarantorController : ControllerBase //ODataController
{
  private readonly ILogger<LoanApplicationGuarantorController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public LoanApplicationGuarantorController(IMediator _mediator,
    ILogger<LoanApplicationGuarantorController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
  }

  [EnableQuery]
  [HttpGet]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<LoanApplicationGuarantorViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    var request = new QueryLoanApplicationGuarantorCommand();

    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }

  
  // [HttpGet("{membershipNo}/verify")]
  // [ProducesResponseType(typeof(CommandResult<VerifyLoanApplicationGuarantorViewModel>), StatusCodes.Status200OK)]
  // [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  // [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  // public async Task<IActionResult> VerifyLoanApplicationGuarantor([FromRoute] string membershipNo)
  // {
  //   var request = new VerifyLoanApplicationGuarantorCommand(membershipNo);
  //
  //   var rsp = await mediator.Send(request);
  //   var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
  //   return result;
  // }
  
  
  //[EnableQuery]
  [HttpPost("{membershipNo}/verify")]
  //[ODataRoute]
  [ProducesResponseType(typeof(CommandResult<VerifyLoanApplicationGuarantorViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> VerifyLoanApplicationGuarantor([FromBody] VerifyLoanApplicationGuarantorCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [EnableQuery]
  [HttpGet("({key})")]
  // [ODataRoute("({key})")]
  [ProducesResponseType(typeof(LoanApplicationGuarantorViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<SingleResult<LoanApplicationGuarantorViewModel>> Get(
    [FromODataUri] [Required] [CustomizeValidator(Skip = true)] string key)
  {
    var request = new QueryLoanApplicationGuarantorCommand();
    var rsp = await mediator.Send(request);
    var queryable = rsp.Response.Where(p => p.Id == key);
    return SingleResult.Create(queryable.ProjectTo<LoanApplicationGuarantorViewModel>(mapper.ConfigurationProvider));
  }

  [HttpPost("create")]
  [ProducesResponseType(typeof(CommandResult<LoanApplicationGuarantorViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Create([FromBody] CreateLoanApplicationGuarantorCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [HttpPost("update")]
  [ProducesResponseType(typeof(CommandResult<LoanApplicationGuarantorViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Update([FromBody] UpdateLoanApplicationGuarantorCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [HttpPost("delete")]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Delete([FromBody] DeleteLoanApplicationGuarantorCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }


  [EnableQuery]
  [HttpGet("{guarantorId}")]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<LoanApplicationGuarantorApprovalViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetGuarantorApprovals([FromRoute] string guarantorId)
  {
    var request = new QueryLoanApplicationGuarantorApprovalCommand(guarantorId);

    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    return result;
  }
  
  // [EnableQuery]
  [HttpGet("{loanApplicationGuarantorId}/details")]
  //[ODataRoute]
  [ProducesResponseType(typeof(ODataResponse<GetLoanApplicationGuarantorViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetGuarantorApproval([FromRoute] string loanApplicationGuarantorId)
  {
    var request = new GetLoanApplicationGuarantorCommand(loanApplicationGuarantorId);

    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    return result;
  }

  [HttpPost("approve")]
  [ProducesResponseType(typeof(CommandResult<LoanApplicationGuarantorApprovalViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateGuarantorApproval([FromBody] LoanApplicationGuarantorApprovalCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }

    [HttpPost("approveTopup")]
    [ProducesResponseType(typeof(CommandResult<LoanTopupGuarantorApprovalViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateGuarantorTopUpApproval([FromBody] LoanTopupGuarantorApprovalCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }
}