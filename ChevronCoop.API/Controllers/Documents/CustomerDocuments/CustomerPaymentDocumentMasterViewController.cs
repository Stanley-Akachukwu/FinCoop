using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AutoMapper;
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
public class CustomerPaymentDocumentMasterViewController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<CustomerPaymentDocumentMasterViewController> logger;
    private readonly IMapper mapper;
    private readonly ChevronCoopDbContext dbContext;
    public CustomerPaymentDocumentMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<CustomerPaymentDocumentMasterViewController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        dbContext = appDb;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<CustomerPaymentDocumentMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(dbContext.CustomerPaymentDocumentMasterView);
    }




}



