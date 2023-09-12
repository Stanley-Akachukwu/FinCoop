using AP.ChevronCoop.AppDomain.Accounting.FinancialCalendars;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.FinancialCalendars;

public partial class UpdateFinancialCalendarCommandValidator : AbstractValidator<UpdateFinancialCalendarCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateFinancialCalendarCommandValidator> logger;
    public UpdateFinancialCalendarCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateFinancialCalendarCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(40).WithMessage("Code must be less than or equal to 40 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must be less than or equal to 128 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.");

        RuleFor(x => x.IsCurrent)
            .NotEmpty().WithMessage("Is current is required.");

        RuleFor(x => x.IsClosed)
            .NotEmpty().WithMessage("Is closed is required.");

        RuleFor(x => x.ClosedByUserName)
            .MaximumLength(128).WithMessage("Closed by user name must be less than or equal to 128 characters.");

        RuleFor(x => x.DateClosed)
            .Must((model, dateClosed) => !model.IsClosed || dateClosed.HasValue)
            .WithMessage("Date closed is required when the model is closed.");


        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.FinancialCalendars.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check Name Exists
            var checkName = dbContext.FinancialCalendars.Any(r => r.Name == data.Name && r.Id != data.Id);
            if (checkName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Calendar name: {data.Name} exist", data.Name));
            }

            // Check Charge Code Exists
            var checkCode = dbContext.FinancialCalendars.Any(r => r.Code == data.Code && r.Id != data.Id);
            if (checkCode)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                        $"Calendar code: {data.Code} exist", data.Code));
            }

        });

    }


}
