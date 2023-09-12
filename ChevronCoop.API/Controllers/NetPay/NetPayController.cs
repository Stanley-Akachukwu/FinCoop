using System;
using AP.ChevronCoop.AppCore.Services.BackgroundServices;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppDomain.NetPay.MemberExposure;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Commons;
using AutoMapper;
using ChevronCoop.API.Config;
using ChevronCoop.API.Controllers.Deposits.Savings.SavingsAccountApplications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using MySqlX.XDevAPI.Common;

namespace ChevronCoop.API.Controllers.NetPay
{
    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "NetPay")]
    [Route("[controller]")]
    [ODataAttributeRouting]
    //[ServiceFilter(typeof(ValidationFilterAttribute))]

    public class NetPayController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<NetPayController> logger;
        private readonly IMapper mapper;
        private readonly IPayrollScheduleBackgroundService payrollScheduleBackgroundService;

        public NetPayController(IMediator _mediator,
        ILogger<NetPayController> _logger, IMapper _mapper, IPayrollScheduleBackgroundService payrollScheduleBackgroundService)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
            this.payrollScheduleBackgroundService = payrollScheduleBackgroundService;
        }

        [HttpPost("memberExposure")]
        [ProducesResponseType(typeof(MemberExposureViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MemberExposureViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MemberExposureViewModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MemberExposure([FromBody] CreateMemberExposureCommand model)
        {
            var response = await mediator.Send(model);

            if (!response.Error)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("monthlyPayrollSchedule")]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MonthlyPayrollSchedule([FromBody] MonthlyPayrollScheduleCommand model)
        {
            var response = await mediator.Send(model);

            if (!response.Error)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("schedule/{scheduleId}")]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MatchPayroll([FromRoute] string scheduleId)
        {
            var response = await payrollScheduleBackgroundService.MatchDeductionAndPayrollData(scheduleId);

            return Ok(response);
        }

        [HttpPost("emailtest/{email}")]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MonthlyPayrollScheduleViewModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EmailTest([FromRoute] string email)
        {
              await payrollScheduleBackgroundService.TestEmailService(email);

            return Ok();
        }
    }
}

