using System;
using AP.ChevronCoop.AppCore.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionMatch;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionMatch
{
    public class MatchDeductionPayrollCommandValidator : AbstractValidator<CreatePayrollDeductionMatchCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<MatchDeductionPayrollCommandValidator> logger;
        public MatchDeductionPayrollCommandValidator(ChevronCoopDbContext appDbContext, ILogger<MatchDeductionPayrollCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.ScheduleId).NotEmpty().NotNull();
        }
    }
}

