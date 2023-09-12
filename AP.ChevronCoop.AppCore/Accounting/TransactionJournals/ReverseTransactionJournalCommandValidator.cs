using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionJournals;

public partial class ReverseTransactionJournalCommandValidator : AbstractValidator<ReverseTransactionJournalCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<ReverseTransactionJournalCommandValidator> logger;
    public ReverseTransactionJournalCommandValidator(ChevronCoopDbContext appDbContext, ILogger<ReverseTransactionJournalCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.TransactionNo).NotEmpty()
            .MaximumLength(40).WithMessage("Transaction No must not exceed 40 characters."); ;


        RuleFor(x => x.Memo)
            .MaximumLength(1024).WithMessage("Memo must not exceed 1024 characters.");

        RuleFor(x => x.Description)
           .MaximumLength(1024).WithMessage("Description must not exceed 1024 characters.");


        RuleFor(p => p).Custom((data, context) =>
        {

            var journal = dbContext.TransactionJournals.Where(r => r.TransactionNo == data.TransactionNo)
           .Include(p => p.JournalEntries).ThenInclude(p => p.Account).FirstOrDefault();

            if (journal == null)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.TransactionNo),
                "Selected Transaction No does not exist", data.TransactionNo));
            }

            if (journal != null && !journal.IsBalanced)
            {
                context.AddFailure(
                   new ValidationFailure(nameof(data.TransactionNo),
                       $"Transaction entries must balance before reversal", data.TransactionNo));

            }

            if (journal != null && !journal.IsPosted)
            {
                context.AddFailure(
                   new ValidationFailure(nameof(data.TransactionNo),
                       $"Transaction entries must be posted before reversal", data.TransactionNo));
            }



            if (journal != null && journal.IsReversed)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.TransactionNo),
                        $"Transaction entries have already been reversed", data.TransactionNo));
            }

        });

    }


}




