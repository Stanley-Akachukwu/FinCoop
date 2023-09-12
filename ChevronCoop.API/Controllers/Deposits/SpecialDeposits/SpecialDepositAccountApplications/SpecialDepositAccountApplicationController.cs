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
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;

namespace ChevronCoop.API.Controllers.Deposits.SpecialDeposits
{




    [ApiController]
    //[ApiVersion("1.0")]
	[ApiExplorerSettings(IgnoreApi = false,GroupName="Deposits")]
    [Route("[controller]")]
     [ODataAttributeRouting]

    public class SpecialDepositAccountApplicationController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<SpecialDepositAccountApplicationController> logger;
        private readonly IMapper mapper;

        public SpecialDepositAccountApplicationController(IMediator _mediator,
        ILogger<SpecialDepositAccountApplicationController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
		[ProducesResponseType(typeof(ODataResponse<SpecialDepositAccountApplicationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
           var request = new QuerySpecialDepositAccountApplicationCommand();

            var rsp = await mediator.Send(request);
			var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }


        [EnableQuery]
        [HttpGet("({key})")]
       // [ODataRoute("({key})")]
		[ProducesResponseType(typeof(SpecialDepositAccountApplicationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<SingleResult<SpecialDepositAccountApplicationViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
        {
			var request = new QuerySpecialDepositAccountApplicationCommand();
            var rsp = await mediator.Send(request);
            var queryable = rsp.Response.Where(p => p.Id == key);
            return SingleResult.Create(queryable.ProjectTo<SpecialDepositAccountApplicationViewModel>(mapper.ConfigurationProvider));

        }

        [HttpPost("create")]
		[ProducesResponseType(typeof(CommandResult<SpecialDepositAccountApplicationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Create([FromBody] CreateSpecialDepositAccountApplicationCommand model)
        {

           var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("update")]
		[ProducesResponseType(typeof(CommandResult<SpecialDepositAccountApplicationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Update([FromBody] UpdateSpecialDepositAccountApplicationCommand model)
        {
           var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("delete")]
		[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Delete([FromBody] DeleteSpecialDepositAccountApplicationCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;

        }


    }
	
}


