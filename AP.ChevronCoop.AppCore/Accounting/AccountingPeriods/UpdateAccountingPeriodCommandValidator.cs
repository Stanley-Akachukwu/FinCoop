using AP.ChevronCoop.AppDomain.Accounting.AccountingPeriods;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.AccountingPeriods;

public partial class UpdateAccountingPeriodCommandValidator : AbstractValidator<UpdateAccountingPeriodCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateAccountingPeriodCommandValidator> logger;
    public UpdateAccountingPeriodCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateAccountingPeriodCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.CalendarId).NotEmpty().WithMessage("CalendarId is required.")
            .MaximumLength(40).WithMessage("CalendarId maximum length is 900 characters.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name maximum length is 256 characters.");

        RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required.")
            .Must(x => x >= DateTime.UtcNow.Date).WithMessage("StartDate should be greater than or equal to today's date.");

        RuleFor(x => x.EndDate).NotEmpty().WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate).WithMessage("EndDate should be greater than StartDate.");

        RuleFor(x => x.ClosedByUserName).NotEmpty().When(x => x.IsClosed == true).WithMessage("ClosedByUserName is required when IsClosed is true.");

        RuleFor(x => x.DateClosed).NotEmpty().When(x => x.IsClosed == true).WithMessage("DateClosed is required when IsClosed is true.")
            .LessThanOrEqualTo(x => DateTime.UtcNow.Date).WithMessage("DateClosed should be less than or equal to today's date.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.AccountingPeriods.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check Calendar Id Exists
            var checkCalendarId = dbContext.FinancialCalendars.Any(r => r.Id == data.CalendarId);
            if (!checkCalendarId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.CalendarId),
                        "Selected calendar Id does not exist", data.CalendarId));
            }


            // Check Name Exists
            var checkName = dbContext.AccountingPeriods.Any(r => r.Name == data.Name && r.Id != data.Id);
            if (checkName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Account period: {data.Name} exist", data.Name));
            }

        });

    }


}
