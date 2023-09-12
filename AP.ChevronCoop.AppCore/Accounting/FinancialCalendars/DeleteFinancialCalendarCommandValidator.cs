using AP.ChevronCoop.AppDomain.Accounting.FinancialCalendars;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.FinancialCalendars;

public partial class DeleteFinancialCalendarCommandValidator : AbstractValidator<DeleteFinancialCalendarCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteFinancialCalendarCommandValidator> logger;
    public DeleteFinancialCalendarCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteFinancialCalendarCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.FinancialCalendars.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }
        });

    }


}
