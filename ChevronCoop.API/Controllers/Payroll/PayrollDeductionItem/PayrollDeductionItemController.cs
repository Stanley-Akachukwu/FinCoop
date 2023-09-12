using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using AP.ChevronCoop.Commons;
using ChevronCoop.API.Config;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionMatch;
using AutoMapper.QueryableExtensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;

namespace ChevronCoop.API.Controllers.Deposits.DepositProducts
{
    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Payroll")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class PayrollDeductionItemController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<PayrollDeductionItemController> logger;
        private readonly IMapper mapper;


        public PayrollDeductionItemController(IMediator _mediator,
        ILogger<PayrollDeductionItemController> _logger, IMapper _mapper,
        IPayrollScheduleBackgroundService payrollScheduleBackgroundService)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
        }


        [HttpPost("import")]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ImportPayrollDeductionItemCommand model)
        {

            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        /// <summary>
        /// API to match deduction to payroll 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("matchPayroll")]
        [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MatchPayrollDeduction([FromBody] CreatePayrollDeductionMatchCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }




        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<PayrollDeductionItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var request = new QueryPayrollDeductionItemCommand();

            var rsp = await mediator.Send(request);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }


        [EnableQuery]
        [HttpGet("({key})")]
        // [ODataRoute("({key})")]
        [ProducesResponseType(typeof(PayrollDeductionItemViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<SingleResult<PayrollDeductionItemViewModel>> Get(
          [FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
        {
            var request = new QueryPayrollDeductionItemCommand();
            var rsp = await mediator.Send(request);
            var queryable = rsp.Response.Where(p => p.Id == key);
            return SingleResult.Create(queryable.ProjectTo<PayrollDeductionItemViewModel>(mapper.ConfigurationProvider));
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CommandResult<PayrollDeductionItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreatePayrollDeductionItemCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult<PayrollDeductionItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdatePayrollDeductionItemCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeletePayrollDeductionItemCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


    }
}






