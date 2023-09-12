using AP.ChevronCoop.AppDomain.Accounting.AccountingPeriods;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.AccountingPeriods;

public partial class DeleteAccountingPeriodCommandValidator : AbstractValidator<DeleteAccountingPeriodCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteAccountingPeriodCommandValidator> logger;
    public DeleteAccountingPeriodCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteAccountingPeriodCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.AccountingPeriods.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }
        });

    }


}
