using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollCronJobConfigs
{
    public partial class UpdatePayrollCronJobConfigCommandValidator : AbstractValidator<UpdatePayrollCronJobConfigCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdatePayrollCronJobConfigCommandValidator> logger;
        public UpdatePayrollCronJobConfigCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdatePayrollCronJobConfigCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(p => p.CronJobType).NotNull().WithMessage("CronJobType is required.");
            RuleFor(p => p.JobName).NotEmpty().WithMessage("JobName is required.");
            RuleFor(p => p.ProcessingDate).NotEmpty().WithMessage("ProcessingDate is required.");
            RuleFor(p => p.ProcessingEndDate).NotEmpty().WithMessage("ProcessingEndDate is required.");
            RuleFor(p => p.UpdatedByUserId).NotNull().WithMessage("UpdatedByUserId is required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var checkExist = dbContext.PayrollCronJobConfigs.Where(r => r.Id == data.Id).Any();
                if (!checkExist)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Invalid Job Id.", data.Id));
                }


            });
        }
    }
}
