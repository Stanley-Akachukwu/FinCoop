using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.JournalEntries;

public partial class DeleteJournalEntryCommandValidator : AbstractValidator<DeleteJournalEntryCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteJournalEntryCommandValidator> logger;
    public DeleteJournalEntryCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteJournalEntryCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.JournalEntries.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
