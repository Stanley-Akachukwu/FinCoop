using AP.ChevronCoop.AppDomain.Accounting.FinancialCalendars;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.FinancialCalendars;

public partial class CreateFinancialCalendarCommandValidator : AbstractValidator<CreateFinancialCalendarCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateFinancialCalendarCommandValidator> logger;
    public CreateFinancialCalendarCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateFinancialCalendarCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(40).WithMessage("Code cannot be longer than 40 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name cannot be longer than 128 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be greater than or equal to start date.");

        RuleFor(x => x.IsCurrent)
            .NotNull().WithMessage("Current status is required.");

        RuleFor(x => x.IsClosed)
            .NotNull().WithMessage("Closed status is required.");

        RuleFor(x => x.ClosedByUserName)
            .NotEmpty().When(x => x.IsClosed).WithMessage("Closed by username is required when the model is closed.")
            .MaximumLength(128).WithMessage("Closed by username cannot be longer than 128 characters.");

        RuleFor(x => x.DateClosed)
            .NotEmpty().When(x => x.IsClosed).WithMessage("Date closed is required when the model is closed.");

        RuleFor(p => p).Custom((data, context) =>
        {
            // Check Name Exists
            var checkName = dbContext.FinancialCalendars.Any(r => r.Name == data.Name);
            if (checkName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Calendar name: {data.Name} exist", data.Name));
            }

            // Check Charge Code Exists
            var checkCode = dbContext.FinancialCalendars.Any(r => r.Code == data.Code);
            if (checkCode)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                        $"Calendar code: {data.Code} exist", data.Code));
            }

        });

    }


}
