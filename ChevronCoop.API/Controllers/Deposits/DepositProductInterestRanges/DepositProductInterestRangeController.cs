using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using ChevronCoop.API.Config;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;

namespace ChevronCoop.API.Controllers.Deposits.DepositProductInterestRanges
{

    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Deposits")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class DepositProductInterestRangeController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<DepositProductInterestRangeController> logger;
        private readonly IMapper mapper;

        public DepositProductInterestRangeController(IMediator _mediator,
        ILogger<DepositProductInterestRangeController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<DepositProductInterestRangeViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var request = new QueryDepositProductInterestRangeCommand();

            var rsp = await mediator.Send(request);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }


        [EnableQuery]
        [HttpGet("({key})")]
        // [ODataRoute("({key})")]
        [ProducesResponseType(typeof(DepositProductInterestRangeViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<SingleResult<DepositProductInterestRangeViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
        {
            var request = new QueryDepositProductInterestRangeCommand();
            var rsp = await mediator.Send(request);
            var queryable = rsp.Response.Where(p => p.Id == key);
            return SingleResult.Create(queryable.ProjectTo<DepositProductInterestRangeViewModel>(mapper.ConfigurationProvider));

        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CommandResult<DepositProductInterestRangeViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateDepositProductInterestRangeCommand model)
        {

            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult<DepositProductInterestRangeViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateDepositProductInterestRangeCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteDepositProductInterestRangeCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;

        }


    }
}


