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
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;

namespace ChevronCoop.API.Controllers.Deposits.SpecialDeposits
{




[ApiController]
    //[ApiVersion("1.0")]
	[ApiExplorerSettings(IgnoreApi = false,GroupName="Deposits")]
    [Route("[controller]")]
     [ODataAttributeRouting]

    public class SpecialDepositInterestScheduleItemController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<SpecialDepositInterestScheduleItemController> logger;
        private readonly IMapper mapper;

        public SpecialDepositInterestScheduleItemController(IMediator _mediator,
        ILogger<SpecialDepositInterestScheduleItemController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
		[ProducesResponseType(typeof(ODataResponse<SpecialDepositInterestScheduleItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
           var request = new QuerySpecialDepositInterestScheduleItemCommand();

            var rsp = await mediator.Send(request);
			var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }


        [EnableQuery]
        [HttpGet("({key})")]
       // [ODataRoute("({key})")]
		[ProducesResponseType(typeof(SpecialDepositInterestScheduleItemViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<SingleResult<SpecialDepositInterestScheduleItemViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
        {
			var request = new QuerySpecialDepositInterestScheduleItemCommand();
            var rsp = await mediator.Send(request);
            var queryable = rsp.Response.Where(p => p.Id == key);
            return SingleResult.Create(queryable.ProjectTo<SpecialDepositInterestScheduleItemViewModel>(mapper.ConfigurationProvider));

        }

        [HttpPost("create")]
		[ProducesResponseType(typeof(CommandResult<SpecialDepositInterestScheduleItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Create([FromBody] CreateSpecialDepositInterestScheduleItemCommand model)
        {

           var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("update")]
		[ProducesResponseType(typeof(CommandResult<SpecialDepositInterestScheduleItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Update([FromBody] UpdateSpecialDepositInterestScheduleItemCommand model)
        {
           var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("delete")]
		[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Delete([FromBody] DeleteSpecialDepositInterestScheduleItemCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;

        }


    }
	
}


