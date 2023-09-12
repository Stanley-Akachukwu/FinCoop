using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using AP.ChevronCoop.Entities.Loans.LoanProducts;

namespace ChevronCoop.API.Controllers.Loans.LoanProducts
{
    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Loans")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class LoanProductMasterViewController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<LoanProductMasterViewController> logger;
        private readonly IMapper mapper;
        private readonly ChevronCoopDbContext dbContext;
        public LoanProductMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
        ILogger<LoanProductMasterViewController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
            dbContext = appDb;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<LoanProductMasterView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(dbContext.LoanProductMasterView);
        }




    }












}