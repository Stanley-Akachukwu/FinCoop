using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Documents.CustomerDocuments;
using AP.ChevronCoop.AppDomain.Documents.DocumentTypes;
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

namespace ChevronCoop.API.Controllers.Documents.CustomerDocuments;

[ApiController]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Docs")]
[Route("[controller]")]
[ODataAttributeRouting]
public class CustomerPaymentDocumentController : ControllerBase //ODataController
{
  private readonly ILogger<CustomerPaymentDocumentController> logger;
  private readonly IMapper mapper;

  private readonly IMediator mediator;

  public CustomerPaymentDocumentController(IMediator _mediator,
    ILogger<CustomerPaymentDocumentController> _logger, IMapper _mapper)
  {
    mediator = _mediator;
    logger = _logger;
    mapper = _mapper;
  }

  [EnableQuery]
  [HttpGet]
  [ProducesResponseType(typeof(ODataResponse<QueryCustomerPaymentDocumentCommand>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get()
  {
    var request = new QueryCustomerPaymentDocumentCommand();

    var rsp = await mediator.Send(request);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
    return result;
  }


  [EnableQuery]
  [HttpGet("({key})")]
  [ProducesResponseType(typeof(QueryCustomerPaymentDocumentCommand), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<SingleResult<QueryCustomerPaymentDocumentCommand>> Get(
    [FromODataUri] [Required] [CustomizeValidator(Skip = true)] string key)
  {
    var request = new QueryDocumentTypeCommand();
    var rsp = await mediator.Send(request);
    var queryable = rsp.Response.Where(p => p.Id == key);
    return SingleResult.Create(queryable.ProjectTo<QueryCustomerPaymentDocumentCommand>(mapper.ConfigurationProvider));
  }

  [HttpPost("create")]
  [ProducesResponseType(typeof(CommandResult<CustomerPaymentDocumentViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Create([FromBody] CreateCustomerPaymentDocumentCommand model)
  {
    var rsp = await mediator.Send(model);
    var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
    return result;
  }
}