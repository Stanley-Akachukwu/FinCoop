using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
public partial class CreateSpecialDepositIncreaseDecreaseCommandValidator : AbstractValidator<CreateSpecialDepositIncreaseDecreaseCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateSpecialDepositIncreaseDecreaseCommandValidator> logger;
    public CreateSpecialDepositIncreaseDecreaseCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSpecialDepositIncreaseDecreaseCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.CustomerId).NotNull().WithMessage("Customer Id is Required");
        RuleFor(p => p.SpecialDepositAccountId).NotNull().WithMessage("Account Id is Required");
        RuleFor(p => p.Amount).NotNull().WithMessage("Amount is Required");
        RuleFor(p => p.ContributionChangeRequest).IsInEnum();


        RuleFor(p => p).Custom((data, context) =>
        {


            var customerExist = dbContext.Customers.Where(r => r.Id == data.CustomerId).Any();

            if (!customerExist)
                context.AddFailure(
                new ValidationFailure(nameof(data.CustomerId),
                "Customer doesn't exist", data.CustomerId));

            var accountExist = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.SpecialDepositAccountId && r.CustomerId == data.CustomerId).Any();
            if (!accountExist)
                context.AddFailure(
                new ValidationFailure(nameof(data.SpecialDepositAccountId),
                "Account doesn't exist", data.SpecialDepositAccountId));

        });

    }


}



