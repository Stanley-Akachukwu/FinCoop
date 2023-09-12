using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionJournals;


public partial class UpdateTransactionJournalCommandValidator : AbstractValidator<UpdateTransactionJournalCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateTransactionJournalCommandValidator> logger;
    public UpdateTransactionJournalCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateTransactionJournalCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.TransactionNo)
            .NotEmpty().WithMessage("Transaction number is required.")
            .MaximumLength(64).WithMessage("Transaction number must not exceed 64 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(512).WithMessage("Title must not exceed 512 characters.");

        RuleFor(x => x.TransactionType).IsEnumName(typeof(TransactionType), false)
            .NotEmpty().WithMessage("Valid Transaction type is required.");


        RuleFor(x => x.DocumentRef)
            .MaximumLength(64).WithMessage("Document reference must not exceed 64 characters.");

        RuleFor(x => x.PostingRef)
            .MaximumLength(64).WithMessage("Posting reference must not exceed 64 characters.");

        RuleFor(x => x.EntityRef)
            .MaximumLength(64).WithMessage("Entity reference must not exceed 64 characters.");

        RuleFor(x => x.TransactionDate)
            .NotEmpty().WithMessage("Transaction date is required.");

        RuleFor(x => x.IsPosted)
            .NotEmpty().WithMessage("IsPosted is required.");

        RuleFor(x => x.IsReversed)
            .NotEmpty().WithMessage("IsReversed is required.");

        RuleFor(x => x.Memo)
            .MaximumLength(1024).WithMessage("Memo must not exceed 1024 characters.");



        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.TransactionJournals.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check TransactionNo Exists
            var checkTransactionNo = dbContext.TransactionJournals
                .Any(r => r.TransactionNo == data.TransactionNo && r.Id != data.Id);
            if (checkTransactionNo)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.TransactionNo),
                        $"Duplicate transaction numbers not allowed", data.TransactionNo));
            }

        });

    }

}
