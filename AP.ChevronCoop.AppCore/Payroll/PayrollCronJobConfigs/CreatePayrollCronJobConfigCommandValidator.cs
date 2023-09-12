using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollCronJobConfigs
{
    public partial class CreatePayrollCronJobConfigCommandValidator : AbstractValidator<CreatePayrollCronJobConfigCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreatePayrollCronJobConfigCommandValidator> logger;
        public CreatePayrollCronJobConfigCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreatePayrollCronJobConfigCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.CronJobType).NotNull().WithMessage("CronJobType is required.");
            RuleFor(p => p.JobName).NotEmpty().WithMessage("JobName is required.");
            RuleFor(p => p.ProcessingDate).NotEmpty().WithMessage("ProcessingDate is required.");
            RuleFor(p => p.ProcessingEndDate).NotEmpty().WithMessage("ProcessingEndDate is required.");
            RuleFor(p => p.CreatedByUserId).NotNull().WithMessage("CreatedByUserId is required.");

        }


    }

}


 
