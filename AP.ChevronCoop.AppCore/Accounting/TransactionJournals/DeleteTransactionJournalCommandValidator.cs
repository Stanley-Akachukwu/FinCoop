using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionJournals;

public partial class DeleteTransactionJournalCommandValidator : AbstractValidator<DeleteTransactionJournalCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteTransactionJournalCommandValidator> logger;
    public DeleteTransactionJournalCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteTransactionJournalCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.TransactionJournals.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
