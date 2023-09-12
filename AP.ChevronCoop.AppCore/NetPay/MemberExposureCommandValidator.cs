using System;
using AP.ChevronCoop.AppCore.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.NetPay.MemberExposure;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.NetPay
{
    public class MemberExposureCommandValidator : AbstractValidator<CreateMemberExposureCommand>
    {
        private readonly ILogger<MemberExposureCommandValidator> _logger;

        public MemberExposureCommandValidator(ILogger<MemberExposureCommandValidator> logger)
        {
            _logger = logger;
            RuleFor(p => p.EmployeeNo).NotEmpty().MaximumLength(20);
            RuleFor(p => p.Month).InclusiveBetween(1, 12);
            RuleFor(p => p.Year).GreaterThanOrEqualTo(2020);
        }
    }
}

