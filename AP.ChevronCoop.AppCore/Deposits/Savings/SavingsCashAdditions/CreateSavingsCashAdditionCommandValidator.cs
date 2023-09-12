using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsCashAdditions;
public partial class CreateSavingsCashAdditionCommandValidator : AbstractValidator<CreateSavingsCashAdditionCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateSavingsCashAdditionCommandValidator> logger;
    public CreateSavingsCashAdditionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSavingsCashAdditionCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;



        RuleFor(p => p.CustomerId).NotNull().WithMessage("Customer Id is Required");
        RuleFor(p => p.SavingsAccountId).NotNull().WithMessage("Account Id is Required");
        RuleFor(p => p.Amount).NotNull().WithMessage("Amount is Required");
        RuleFor(p => p.ModeOfPayment).IsInEnum();



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

            var customerExist = dbContext.Customers.Where(r => r.Id == data.CustomerId).Any();

            if (!customerExist)
                context.AddFailure(
                new ValidationFailure(nameof(data.CustomerId),
                "Customer doesn't exist", data.CustomerId));

            var accountExist = dbContext.SavingsAccounts.Where(r => r.Id == data.SavingsAccountId && r.CustomerId == data.CustomerId).Any();
            if (!accountExist)
                context.AddFailure(
                new ValidationFailure(nameof(data.SavingsAccountId),
                "Account doesn't exist", data.SavingsAccountId));

            if (data.ModeOfPayment == DepositFundingSourceType.SPECIAL_DEPOSIT)
            {
                var account = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.ModeOfPaymentAccountId && r.CustomerId == data.CustomerId)
               .FirstOrDefault();

                if (account == null)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.ModeOfPaymentAccountId),
                            "Special Deposit Account doesn't exist for customer", data.ModeOfPaymentAccountId));
                }

            }

        });

    }


}



