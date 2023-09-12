using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.MasterData.Departments;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AutoMapper;
using ChevronCoop.API.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace ChevronCoop.API.Controllers.Loans.LoanProducts;

[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Loans")]
[Route("[controller]")]
[ODataAttributeRouting]
public class LoanProductController : ControllerBase //ODataController
{
    private readonly ILogger<LoanProductController> logger;
    private readonly IMapper mapper;

    private readonly IMediator mediator;

    public LoanProductController(IMediator _mediator,
      ILogger<LoanProductController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<LoanProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var request = new QueryLoanProductCommand();

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }

    // [EnableQuery]
    [HttpGet("{loanType}/userProducts/{applicationUserId}")]
    //[ODataRoute]
    [ProducesResponseType(typeof(CommandResult<GetUserLoanProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserLoanProducts([FromRoute] LoanProductType loanType, [FromRoute] string applicationUserId)
    {
        var request = new GetUserLoanProductsCommand(loanType, applicationUserId);

        var rsp = await mediator.Send(request);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
        return result;
    }


    // [EnableQuery]
    // [ODataRoute("({key})")]
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(GetLoanProductViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProduct([FromRoute] string productId)
    {
        var request = new GetLoanProductCommand(productId);
        var rsp = await mediator.Send(request);
        return await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommandResult<LoanProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateLoanProductCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("update")]
    [ProducesResponseType(typeof(CommandResult<LoanProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateLoanProductCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }


    [HttpPost("delete")]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteLoanProductCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }

    [HttpPost("publish")]
    [ProducesResponseType(typeof(CommandResult<LoanProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Publish([FromBody] PublishLoanProductCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }

    [HttpGet("{productId}/customerPublication")]
    [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomerPublicationByProduct([FromRoute] string productId)
    {
        var request = new GetCustomerPublicationByProductIdCommand(productId);
        var rsp = await mediator.Send(request);
        return await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    }

    [HttpGet("{productId}/departmentPublication")]
    [ProducesResponseType(typeof(DepartmentViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDepartmentPublicationByProduct([FromRoute] string productId)
    {
        var request = new GetDepartmentPublicationByProductIdCommand(productId);
        var rsp = await mediator.Send(request);
        return await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    }

    [HttpPost("updateStatus")]
    [ProducesResponseType(typeof(CommandResult<LoanProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateLoanProductStatusCommand model)
    {
        var rsp = await mediator.Send(model);
        var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
        return result;
    }

    [HttpPost("customerLoanProductEligibility")]
    [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCustomerLoanProductStatus([FromBody] CustomerLoanProductEligibilityCommand model)
    {
        var rsp = await mediator.Send(model);
        return await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    }
}