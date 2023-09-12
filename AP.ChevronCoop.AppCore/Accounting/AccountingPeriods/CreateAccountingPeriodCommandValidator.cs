using AP.ChevronCoop.AppDomain.Accounting.AccountingPeriods;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.AccountingPeriods;

public partial class CreateAccountingPeriodCommandValidator : AbstractValidator<CreateAccountingPeriodCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateAccountingPeriodCommandValidator> logger;
    public CreateAccountingPeriodCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateAccountingPeriodCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.CalendarId)
            .NotEmpty().WithMessage("CalendarId is required.")
            .MaximumLength(40).WithMessage("CalendarId must be at most 40 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must be at most 128 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate must be greater than or equal to StartDate.");

        RuleFor(x => x.IsCurrent)
            .NotNull().WithMessage("IsCurrent is required.");

        RuleFor(x => x.IsClosed)
            .NotNull().WithMessage("IsClosed is required.");

        RuleFor(x => x.ClosedByUserName)
            .MaximumLength(256).WithMessage("ClosedByUserName must be at most 256 characters when provided.");

        RuleFor(x => x.DateClosed)
            .Must((model, dateClosed) => !model.IsClosed || dateClosed.HasValue).WithMessage("DateClosed is required when IsClosed is true.")
            .When(model => model.IsClosed).WithMessage("DateClosed is required when IsClosed is true.");


        RuleFor(p => p).Custom((data, context) =>
        {
            // Check Calendar Id Exists
            var checkCalendarId = dbContext.FinancialCalendars.Any(r => r.Id == data.CalendarId);
            if (!checkCalendarId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.CalendarId),
                        "Selected calendar Id does not exist", data.CalendarId));
            }

            // Check Name Exists
            var checkName = dbContext.AccountingPeriods.Any(r => r.Name == data.Name);
            if (checkName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Account period: {data.Name} exist", data.Name));
            }
        });

    }


}