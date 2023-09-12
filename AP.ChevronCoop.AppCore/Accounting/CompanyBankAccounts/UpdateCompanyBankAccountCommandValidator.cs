using AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.CompanyBankAccounts;

public partial class UpdateCompanyBankAccountCommandValidator : AbstractValidator<UpdateCompanyBankAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateCompanyBankAccountCommandValidator> logger;
    public UpdateCompanyBankAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateCompanyBankAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        //RuleFor(account => account.LedgerAccountId)
        //    .NotEmpty().WithMessage("Ledger account ID is required.")
        //    .MaximumLength(40).WithMessage("Ledger account ID must not exceed 40 characters.");

        RuleFor(account => account.BankId)
            .NotEmpty().WithMessage("Bank ID is required.")
            .MaximumLength(40).WithMessage("Bank ID must not exceed 40 characters.");

        RuleFor(account => account.BranchName)
            .MaximumLength(128).WithMessage("Branch name must not exceed 128 characters.");

        RuleFor(account => account.BranchAddress)
            .MaximumLength(256).WithMessage("Branch address must not exceed 256 characters.");

        RuleFor(account => account.CurrencyId)
            .NotEmpty().WithMessage("Currency ID is required.")
            .MaximumLength(40).WithMessage("Currency ID must not exceed 40 characters.");

        RuleFor(account => account.AccountName)
            .NotEmpty().WithMessage("Account name is required.")
            .MaximumLength(128).WithMessage("Account name must not exceed 128 characters.");

        RuleFor(account => account.AccountNumber)
            .NotEmpty().WithMessage("Account number is required.")
            .MaximumLength(32).WithMessage("Account number must not exceed 32 characters.");

        RuleFor(account => account.BVN)
            .MaximumLength(32).WithMessage("Account BVN must not exceed 32 characters.");


        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.CompanyBankAccounts.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

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
               .Any(r => r.AccountNumber == data.AccountNumber && r.Id != data.Id);
            if (checkAccount)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.AccountNumber),
                        "Duplicate account number is not allowed.", data.AccountNumber));
            }

            var checkAccount2 = dbContext.CompanyBankAccounts
                .Any(r => (r.BVN == data.BVN && r.AccountNumber == data.AccountNumber) && r.Id != data.Id);
            if (checkAccount2)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.BVN),
                        "Duplicate account no/BVN is not allowed.", data.BVN));
            }

        });

    }


}
