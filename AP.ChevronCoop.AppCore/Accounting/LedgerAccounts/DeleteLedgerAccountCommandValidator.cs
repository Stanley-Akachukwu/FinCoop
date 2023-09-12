using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.LedgerAccounts;

public partial class DeleteLedgerAccountCommandValidator : AbstractValidator<DeleteLedgerAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteLedgerAccountCommandValidator> logger;
    public DeleteLedgerAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteLedgerAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.LedgerAccounts.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
