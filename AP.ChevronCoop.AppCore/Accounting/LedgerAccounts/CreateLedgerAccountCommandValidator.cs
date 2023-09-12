using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.LedgerAccounts;

public partial class CreateLedgerAccountCommandValidator : AbstractValidator<CreateLedgerAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateLedgerAccountCommandValidator> logger;
    public CreateLedgerAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateLedgerAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        //RuleFor(a => a.AccountType).IsInEnum();
        RuleFor(a => a.AccountType).Must(p => System.Enum.IsDefined(typeof(COAType), p)).WithMessage("Invalid COAType not allowed")
           .NotEmpty().WithMessage("Account Type is required.")
           .MaximumLength(32).WithMessage("Account Type must not exceed 32 characters.");

        RuleFor(a => a.UOM)
            .NotEmpty().WithMessage("UOM is required.")
            .MaximumLength(32).WithMessage("UOM must not exceed 32 characters.");

        RuleFor(a => a.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(64).WithMessage("Code must not exceed 64 characters.");

        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must not exceed 128 characters.");

        //RuleFor(a => a.ClearedBalance)
        //    .NotNull().WithMessage("Cleared Balance is required.");

        //RuleFor(a => a.UnclearedBalance)
        //    .NotNull().WithMessage("Uncleared Balance is required.");

        //RuleFor(a => a.LedgerBalance)
        //    .NotNull().WithMessage("Ledger Balance is required.");

        //RuleFor(a => a.AvailableBalance)
        //    .NotNull().WithMessage("Available Balance is required.");

        //RuleFor(a => a.IsOfficeAccount)
        //    .NotEmpty().WithMessage("Is Office Account is required.");

        //RuleFor(a => a.AllowManualEntry)
        //    .NotEmpty().WithMessage("Allow Manual Entry is required.");

        //RuleFor(a => a.IsClosed).Must(x => x == false || x == true).WithMessage("Is Closed is required.");

        //RuleFor(a => a.ClosedByUserName)
        //    .MaximumLength(128).WithMessage("Closed By User Name must not exceed 128 characters.");


        RuleFor(p => p).Custom((data, context) =>
        {
            // Check Name Exists
            var checkName = dbContext.LedgerAccounts.Any(r => r.Name == data.Name);
            if (checkName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Ledger Account name: {data.Name} exist", data.Name));
            }

            // Check Charge Code Exists
            var checkCode = dbContext.LedgerAccounts.Any(r => r.Code == data.Code);
            if (checkCode)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                        $"Ledger Account code: {data.Code} exist", data.Code));
            }
        });

    }


}
