using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsIncreaseDecreases;
public partial class CreateSavingsIncreaseDecreaseCommandValidator : AbstractValidator<CreateSavingsIncreaseDecreaseCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateSavingsIncreaseDecreaseCommandValidator> logger;
    public CreateSavingsIncreaseDecreaseCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSavingsIncreaseDecreaseCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.CustomerId).NotNull().WithMessage("Customer Id is Required");
        RuleFor(p => p.SavingsAccountId).NotNull().WithMessage("Account Id is Required");
        RuleFor(p => p.Amount).NotNull().WithMessage("Amount is Required");
        RuleFor(p => p.ContributionChangeRequest).IsInEnum();


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

        });

    }


}



