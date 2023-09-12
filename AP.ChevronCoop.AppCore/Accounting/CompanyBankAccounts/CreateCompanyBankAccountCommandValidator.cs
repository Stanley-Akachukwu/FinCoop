using AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.CompanyBankAccounts;


public partial class CreateCompanyBankAccountCommandValidator : AbstractValidator<CreateCompanyBankAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateCompanyBankAccountCommandValidator> logger;
    public CreateCompanyBankAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateCompanyBankAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        //RuleFor(x => x.LedgerAccountId).NotEmpty().WithMessage("Ledger Account ID is required.")
        //    .MaximumLength(40).WithMessage("Ledger Account ID must not exceed 40 characters.");

        RuleFor(x => x.BankId).NotEmpty().WithMessage("Bank ID is required.")
            .MaximumLength(40).WithMessage("Bank ID must not exceed 40 characters.");

        RuleFor(x => x.BranchName).MaximumLength(128).WithMessage("Branch Name must not exceed 128 characters.");

        RuleFor(x => x.BranchAddress).MaximumLength(128).WithMessage("Branch Address must not exceed 128 characters.");

        RuleFor(x => x.CurrencyId).NotEmpty().WithMessage("Currency ID is required.")
            .MaximumLength(40).WithMessage("Currency ID must not exceed 40 characters.");

        RuleFor(x => x.AccountName).NotEmpty().WithMessage("Account Name is required.")
            .MaximumLength(128).WithMessage("Account Name must not exceed 128 characters.");

        RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("Account Number is required.")
            .MaximumLength(32).WithMessage("Account Number must not exceed 32 characters.");

        RuleFor(x => x.BVN).MaximumLength(32).WithMessage("Account BVN must not exceed 32 characters.");

        RuleFor(p => p).Custom((data, context) =>
        {
            // Check LedgerAccountId Exists
            //var checkLedgerAccountId = dbContext.LedgerAccounts.Any(r => r.Id == data.LedgerAccountId);
            //if (!checkLedgerAccountId)
            //{
            //    context.AddFailure(
            //        new ValidationFailure(nameof(data.LedgerAccountId),
            //            "Selected Ledger Account Id does not exist", data.LedgerAccountId));
            //}

            // Check BankId Exists
            var checkBankId = dbContext.Banks.Any(r => r.Id == data.BankId);
            if (!checkBankId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.BankId),
                        "Selected Bank Id does not exist", data.BankId));
            }


            // Check Currency Id Exists
            var checkCurrencyId = dbContext.Currencies.Any(r => r.Id == data.CurrencyId);
            if (!checkCurrencyId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.CurrencyId),
                        "Selected currency Id does not exist", data.CurrencyId));
            }

            var checkAccount = dbContext.CompanyBankAccounts
                .Any(r => r.AccountNumber == data.AccountNumber);
            if (checkAccount)
            {
                context.AddFailure(
                   new ValidationFailure(nameof(data.AccountNumber),
                       "Duplicate account number is not allowed.", data.AccountNumber));
            }


            var checkAccount2 = dbContext.CompanyBankAccounts
                .Any(r => r.BVN == data.BVN && r.AccountNumber == data.AccountNumber);
            if (checkAccount2)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.BVN),
                        "Duplicate account no/BVN is not allowed.", data.BVN));
            }

        });

    }


}
