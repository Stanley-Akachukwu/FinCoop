using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.JournalEntries;

public partial class CreateJournalEntryCommandValidator : AbstractValidator<CreateJournalEntryCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateJournalEntryCommandValidator> logger;
    public CreateJournalEntryCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateJournalEntryCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        //RuleFor(x => x.TransactionEntryNo)
        //    .NotEmpty().WithMessage("Transaction entry number is required.")
        //    .MaximumLength(64).WithMessage("Transaction entry number must not exceed 64 characters.");

        //RuleFor(x => x.TransactionJournalId)
        //    .NotEmpty().WithMessage("Transaction journal ID is required.")
        //    .MaximumLength(40).WithMessage("Transaction journal ID must not exceed 40 characters.");

        //RuleFor(x => x.AccountId)
        //    .NotEmpty().WithMessage("Account ID is required.")
        //    .MaximumLength(40).WithMessage("Account ID must not exceed 40 characters.");

        RuleFor(x => x.AccountCode)
           .NotEmpty().WithMessage("Account Code is required.")
           .MaximumLength(40).WithMessage("Account Code must not exceed 40 characters.");

        RuleFor(x => x.EntryType).IsEnumName(typeof(TransactionEntryType), false)
            .NotEmpty().WithMessage("Valid Entry type is required.");
        //.MaximumLength(32).WithMessage("Entry type must not exceed 32 characters.");

        //RuleFor(x => x.DecimalPlaces)
        //    .GreaterThanOrEqualTo(0).WithMessage("Decimal places must be greater than or equal to zero.");

        RuleFor(x => x.Debit)
            .GreaterThanOrEqualTo(0).WithMessage("Debit must be greater than or equal to zero.");

        RuleFor(x => x.Credit)
            .GreaterThanOrEqualTo(0).WithMessage("Credit must be greater than or equal to zero.");

        RuleFor(x => x.TransactionDate)
            .NotEmpty().WithMessage("Transaction date is required.")
            .Must(date => date <= DateTime.Now).WithMessage("Transaction date cannot be in the future.");

        RuleFor(x => x.Memo)
            .MaximumLength(1024).WithMessage("Memo must not exceed 1024 characters.");

        RuleFor(p => p).Custom((data, context) =>
        {
            // Check TransactionJournalId Exists
            //var checkTransactionJournalId = dbContext.TransactionJournals.Any(r => r.Id == data.TransactionJournalId);
            //if (!checkTransactionJournalId)
            //{
            //    context.AddFailure(
            //        new ValidationFailure(nameof(data.TransactionJournalId),
            //            "Selected Transaction Journal Id does not exist", data.TransactionJournalId));
            //}

            // Check AccountId Exists
            //var checkAccountId = dbContext.LedgerAccounts.Any(r => r.Id == data.AccountId);
            //if (!checkAccountId)
            //{
            //    context.AddFailure(
            //        new ValidationFailure(nameof(data.AccountId),
            //            "Selected Ledger Account Id does not exist", data.AccountId));
            //}



            // Check AccountCpde Exists
            var checkAccountCode = dbContext.LedgerAccounts.Any(r => r.Code == data.AccountCode);
            if (!checkAccountCode)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.AccountCode),
                        "Selected Ledger Account Code does not exist", data.AccountCode));
            }

            // Check TransactionEntryNo Exists
            //var checkTransactionNo = dbContext.JournalEntries.Any(r => r.TransactionEntryNo == data.TransactionEntryNo);
            //if (checkTransactionNo)
            //{
            //    context.AddFailure(
            //        new ValidationFailure(nameof(data.TransactionEntryNo),
            //            $"Duplicate Transaction Entry Nos are not allowed", data.TransactionEntryNo));
            //}

            var checkDrCrZero = data.Credit <= 0 && data.Debit <= 0;
            if (checkDrCrZero)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Debit),
                        $"Debit and Credit cannot be less than or equal zero AT THE SAME TIME", data.Debit));
            }

        });

    }


}
