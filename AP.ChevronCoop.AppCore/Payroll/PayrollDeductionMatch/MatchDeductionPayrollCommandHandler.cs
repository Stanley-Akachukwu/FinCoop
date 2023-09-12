using System;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.BackgroundServices;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionMatch;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionMatch
{
    public class MatchDeductionPayrollCommandHandler :

        IRequestHandler<CreatePayrollDeductionMatchCommand, CommandResult<bool>>
    {
        private readonly ILoggerService _logger;
        private readonly ChevronCoopDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMediator _mediator;
        private readonly IPayrollScheduleBackgroundService _payrollScheduleBackgroundService;

        public MatchDeductionPayrollCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator,
            ILoggerService logger, IMapper _mapper, IPayrollScheduleBackgroundService payrollScheduleBackgroundService)
        {
            dbContext = appDbContext;
            _logger = logger;
            mapper = _mapper;
            _mediator = mediator;
            _payrollScheduleBackgroundService = payrollScheduleBackgroundService;
        }


        public async Task<CommandResult<bool>> Handle(CreatePayrollDeductionMatchCommand request, CancellationToken cancellationToken)
        {
            var command = new CommandResult<bool>();

            bool runPayrollResponse = await _payrollScheduleBackgroundService.MatchDeductionAndPayrollData(request.ScheduleId);

            command.Response = runPayrollResponse;
            return command;
        }
    }
}
