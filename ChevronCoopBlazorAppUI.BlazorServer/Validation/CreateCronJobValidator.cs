using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class CreateCronJobValidator : AbstractValidator<CreateJobDTO>
    {
        public CreateCronJobValidator()
        {
            RuleFor(p => p.JobName).NotEmpty().WithMessage("Job Name is required");
            RuleFor(p => p.CronJobType).NotEmpty().WithMessage("CronJobType is required");
            RuleFor(p => p.ProcessingDate).NotEmpty().WithMessage("Process date is required").GreaterThan(p => DateTime.Now).WithMessage("Invalid Process Date");
            RuleFor(p => p.ProcessingTime).NotEmpty().WithMessage("Process time is required");




        }
    }
}
