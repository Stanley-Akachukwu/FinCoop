using AP.ChevronCoop.AppCore.Accounting.JournalEntries;
using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionJournals;

public partial class CreateTransactionJournalCommandValidator : AbstractValidator<CreateTransactionJournalCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateTransactionJournalCommandValidator> logger;
    private readonly ILoggerFactory loggerFactory;
    public CreateTransactionJournalCommandValidator(ChevronCoopDbContext appDbContext,
        ILogger<CreateTransactionJournalCommandValidator> _logger,
        ILoggerFactory _loggerFactory)
    {

        dbContext = appDbContext;
        logger = _logger;
        loggerFactory = _loggerFactory;

        //RuleFor(x => x.TransactionNo)
        //    .NotEmpty().WithMessage("Transaction number is required.")
        //    .MaximumLength(64).WithMessage("Transaction number must not exceed 64 characters.");

        RuleFor(x => x.TransactionType).IsEnumName(typeof(TransactionType), false)
            .NotEmpty().WithMessage("Valid Transaction type is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(512).WithMessage("Title must not exceed 512 characters.");

        RuleFor(x => x.DocumentRef)
            .MaximumLength(64).WithMessage("Document reference must not exceed 64 characters.");

        RuleFor(x => x.PostingRef)
            .MaximumLength(64).WithMessage("Posting reference must not exceed 64 characters.");

        RuleFor(x => x.EntityRef)
            .MaximumLength(64).WithMessage("Entity reference must not exceed 64 characters.");

        RuleFor(x => x.TransactionDate)
            .NotEmpty().WithMessage("Transaction date is required.")
            .Must(date => date <= DateTime.Now).WithMessage("Transaction date cannot be in the future.");

        //RuleFor(x => x.IsPosted).Must(isPosted => isPosted == false).WithMessage("Is Posted must be false for new journals.")
        //    .NotEmpty().WithMessage("IsPosted is required.");

        //RuleFor(x => x.IsReversed).Must(isReversed => isReversed == false).WithMessage("Is Reversed must be false for new journals.")
        //    .NotEmpty().WithMessage("IsReversed is required.");

        RuleFor(x => x.Memo)
            .MaximumLength(1024).WithMessage("Memo must not exceed 1024 characters.");

        RuleForEach(x => x.JournalEntries).NotEmpty().SetValidator(new CreateJournalEntryCommandValidator(dbContext, loggerFactory.CreateLogger<CreateJournalEntryCommandValidator>()));

        RuleFor(p => p).Custom((data, context) =>
        {
            // Check TransactionNo Exists
            //var checkTransactionNo = dbContext.TransactionJournals.Any(r => r.TransactionNo == data.TransactionNo);
            //if (checkTransactionNo)
            //{
            //    context.AddFailure(
            //        new ValidationFailure(nameof(data.TransactionNo),
            //            $"Duplicate transaction numbers not allowed", data.TransactionNo));
            //}

            if (!data.IsBalanced)
            {
                context.AddFailure(
                                   new ValidationFailure(nameof(data.TransactionNo),
                                       $"Journal entries are not balanced. Debits must be equal to Credits", data.TransactionNo));
            }



        });
    }


}
