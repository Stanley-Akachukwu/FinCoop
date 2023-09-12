
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
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
using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.API.Controllers.Deposits.Savings.SavingsAccountApplications;

[ApiController]
    //[ApiVersion("1.0")]
	[ApiExplorerSettings(IgnoreApi = false,GroupName="Deposits")]
    [Route("[controller]")]
     [ODataAttributeRouting]

    public class SavingsAccountApplicationController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<SavingsAccountApplicationController> logger;
        private readonly IMapper mapper;

        public SavingsAccountApplicationController(IMediator _mediator,
        ILogger<SavingsAccountApplicationController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
		[ProducesResponseType(typeof(ODataResponse<SavingsAccountApplicationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
           var request = new QuerySavingsAccountApplicationCommand();

            var rsp = await mediator.Send(request);
			var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }


        [EnableQuery]
        [HttpGet("({key})")]
       // [ODataRoute("({key})")]
		[ProducesResponseType(typeof(SavingsAccountApplicationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<SingleResult<SavingsAccountApplicationViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
        {
			var request = new QuerySavingsAccountApplicationCommand();
            var rsp = await mediator.Send(request);
            var queryable = rsp.Response.Where(p => p.Id == key);
            return SingleResult.Create(queryable.ProjectTo<SavingsAccountApplicationViewModel>(mapper.ConfigurationProvider));

        }

        [HttpPost("create")]
		[ProducesResponseType(typeof(CommandResult<SavingsAccountApplicationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Create([FromBody] CreateSavingsAccountApplicationCommand model)
        {

           var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("update")]
		[ProducesResponseType(typeof(CommandResult<SavingsAccountApplicationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Update([FromBody] UpdateSavingsAccountApplicationCommand model)
        {
           var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("delete")]
		[ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Delete([FromBody] DeleteSavingsAccountApplicationCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;

        }


    }
	
		
 
