using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositAccountApplications;
public partial class CreateFixedDepositAccountApplicationCommandValidator : AbstractValidator<CreateFixedDepositAccountApplicationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateFixedDepositAccountApplicationCommandValidator> logger;
    public CreateFixedDepositAccountApplicationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateFixedDepositAccountApplicationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.CustomerId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.DepositProductId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.Amount).NotNull();
        RuleFor(p => p.InterestRate).NotNull();

        //RuleFor(p => p.TenureUnit).NotEmpty().MaximumLength(128);
        //RuleFor(p => p.TenureValue).NotNull();


        RuleFor(p => p.LiquidationAccountType).NotEmpty().WithMessage("Liquidation Account Type is required.");
        RuleFor(p => p.LiquidationAccountId).NotEmpty().WithMessage("Liquidation Account is required");

        When(x => x.ModeOfPayment == DepositFundingSourceType.SPECIAL_DEPOSIT, () =>
        {
            RuleFor(p => p.ModeOfPaymentAccountId).NotEmpty().WithMessage("Mode of payment account is required");
        });

        When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER, () =>
        {
            RuleFor(x => x.Document).NotEmpty().WithMessage("Document is required.");
            RuleFor(x => x.FileName).NotEmpty().WithMessage("Document fileName is required.");
            RuleFor(x => x.MimeType).NotEmpty().WithMessage("Document mimetype is required.");


        });

   

        RuleFor(p => p).Custom((data, context) =>
        {

            var existingAccount = dbContext.FixedDepositAccounts.Where(r => r.CustomerId == data.CustomerId).FirstOrDefault();
            if (existingAccount != null)
            {
                if (existingAccount.DepositProductId == data.DepositProductId)
                    context.AddFailure(
                     new ValidationFailure(nameof(data.CustomerId),
                         "Multiple Accounts on same product not allowed.", data.CustomerId));
            }
            var depositProduct = dbContext.DepositProducts.Where(r => r.Id == data.DepositProductId).FirstOrDefault();

            if (depositProduct == null)
            {
                context.AddFailure(
                     new ValidationFailure(nameof(data.DepositProductId),
                         "Selected Deposit Product not found.", data.DepositProductId));

            }

            var customer = dbContext.Customers.Where(r => r.Id == data.CustomerId).FirstOrDefault();

            if (customer == null)
            {
                context.AddFailure(
                     new ValidationFailure(nameof(data.CustomerId),
                         "Customer not found.", data.CustomerId));
            }

            if (data.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT)
            {
                var account = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.LiquidationAccountId && r.CustomerId == data.CustomerId)
               .FirstOrDefault();

                if (account == null)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.LiquidationAccountId),
                            "Special Deposit Account doesn't exist for customer", data.LiquidationAccountId));
                }

            }

            if (data.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT)
            {
                var account = dbContext.SavingsAccounts.Where(r => r.Id == data.LiquidationAccountId && r.CustomerId == data.CustomerId)
               .FirstOrDefault();

                if (account == null)
                {

                    context.AddFailure(
                        new ValidationFailure(nameof(data.LiquidationAccountId),
                            "Savings Account doesn't exist for customer", data.LiquidationAccountId));
                }


            }


            if (data.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
            {
                var account = dbContext.CustomerBankAccounts.Where(r => r.Id == data.LiquidationAccountId && r.CustomerId == data.CustomerId)
               .FirstOrDefault();

                if (account == null)
                {


                    context.AddFailure(
                    new ValidationFailure(nameof(data.LiquidationAccountId),
                        "Bank Account doesn't exist for customer", data.LiquidationAccountId));
                }

            }



            if (data.ModeOfPayment == DepositFundingSourceType.SPECIAL_DEPOSIT)
            {

                var account = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.ModeOfPaymentAccountId && r.CustomerId == data.CustomerId)
                .Include(x => x.DepositAccount)
               .FirstOrDefault();
                if (account == null)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.ModeOfPaymentAccountId),
                            "Special Deposit Account doesn't exist for customer", data.ModeOfPaymentAccountId));
                }

                if (account != null)
                {
                    if (data.Amount > account.DepositAccount.LedgerBalance)
                        context.AddFailure(
                        new ValidationFailure(nameof(data.LiquidationAccountId),
                            "Insuffient fund in savings account", data.LiquidationAccountId));
                }

            }

        });

    }


}



