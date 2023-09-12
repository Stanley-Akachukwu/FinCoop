using System;
using AP.ChevronCoop.AppDomain.NetPay.MemberExposure;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.NetPay
{
    public class MonthlyPayrollScheduleCommandValidator : AbstractValidator<MonthlyPayrollScheduleCommand>
    {
        private readonly ILogger<MonthlyPayrollScheduleCommandValidator> _logger;

        public MonthlyPayrollScheduleCommandValidator(ILogger<MonthlyPayrollScheduleCommandValidator> logger)
        {
            _logger = logger;
            RuleFor(p => p.Month).InclusiveBetween(1, 12);
            RuleFor(p => p.Year).GreaterThanOrEqualTo(2020);
        }
    }
}

