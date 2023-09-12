using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.JournalEntries;

public partial class UpdateJournalEntryCommandValidator : AbstractValidator<UpdateJournalEntryCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateJournalEntryCommandValidator> logger;
    public UpdateJournalEntryCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateJournalEntryCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.TransactionEntryNo)
            .NotEmpty().WithMessage("Transaction entry number is required.")
            .MaximumLength(64).WithMessage("Transaction entry number must not exceed 64 characters.");

        RuleFor(x => x.TransactionJournalId)
            .NotEmpty().WithMessage("Transaction journal ID is required.")
            .MaximumLength(40).WithMessage("Transaction journal ID must not exceed 40 characters.");

        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account ID is required.")
            .MaximumLength(40).WithMessage("Account ID must not exceed 40 characters.");

        RuleFor(x => x.EntryType).IsEnumName(typeof(TransactionEntryType), false)
            .NotEmpty().WithMessage("Valid Entry type is required.");
        //.MaximumLength(32).WithMessage("Entry type must not exceed 32 characters.");

        RuleFor(x => x.DecimalPlaces)
            .NotEmpty().WithMessage("Decimal places are required.");

        RuleFor(x => x.Debit)
            .NotEmpty().WithMessage("Debit amount is required.");

        RuleFor(x => x.Credit)
            .NotEmpty().WithMessage("Credit amount is required.");

        RuleFor(x => x.TransactionDate)
            .NotEmpty().WithMessage("Transaction date is required.");

        RuleFor(x => x.Memo)
            .MaximumLength(1024).WithMessage("Memo must not exceed 1024 characters.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.JournalEntries.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check TransactionJournalId Exists
            var checkTransactionJournalId = dbContext.TransactionJournals.Any(r => r.Id == data.TransactionJournalId);
            if (!checkTransactionJournalId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.TransactionJournalId),
                        "Selected Transaction Journal Id does not exist", data.TransactionJournalId));
            }

            // Check AccountId Exists
            var checkAccountId = dbContext.LedgerAccounts.Any(r => r.Id == data.AccountId);
            if (!checkAccountId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.AccountId),
                        "Selected Ledger Account Id does not exist", data.AccountId));
            }

            // Check TransactionEntryNo Exists
            var checkTransactionNo = dbContext.JournalEntries
                .Any(r => r.TransactionEntryNo == data.TransactionEntryNo && r.Id != data.Id);
            if (checkTransactionNo)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.TransactionEntryNo),
                        $"Transaction Entry No: {data.TransactionEntryNo} exist", data.TransactionEntryNo));
            }

        });

    }


}
