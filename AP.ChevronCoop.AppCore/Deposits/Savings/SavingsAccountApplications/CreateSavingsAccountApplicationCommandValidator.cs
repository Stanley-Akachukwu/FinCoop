using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccountApplications;
public partial class CreateSavingsAccountApplicationCommandValidator : AbstractValidator<CreateSavingsAccountApplicationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateSavingsAccountApplicationCommandValidator> logger;
    public CreateSavingsAccountApplicationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSavingsAccountApplicationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.CustomerId).NotEmpty().WithMessage("Customer Id required");
        RuleFor(p => p.DepositProductId).NotEmpty().WithMessage("Deposit product Id required");
        RuleFor(p => p.Amount).NotNull().WithMessage("Amount required");


        RuleFor(p => p).Custom((data, context) =>
        {


            var existingAccount = dbContext.SavingsAccounts.Where(r => r.CustomerId == data.CustomerId).FirstOrDefault();
            if (existingAccount != null)
            {
                if (existingAccount.DepositProductId == data.DepositProductId)
                    context.AddFailure(
                     new ValidationFailure(nameof(data.CustomerId),
                         "Multiple Accounts on same product not allowed.", data.CustomerId));
            }

            var depositProductExist = dbContext.DepositProducts.Where(m => m.Id == data.DepositProductId).Any();
            if (!depositProductExist) context.AddFailure(new ValidationFailure(nameof(data.DepositProductId), "Selected Deposit Product Doesn't exist.", data.DepositProductId));

            var customer = dbContext.Customers.Where(m => m.Id == data.CustomerId).FirstOrDefault();
            if (customer == null) context.AddFailure(new ValidationFailure(nameof(data.CustomerId), "Customer Doesn't exist", data.CustomerId));

            if (customer?.MemberType == MemberType.REGULAR && data.Amount < 50000)
                context.AddFailure(new ValidationFailure(nameof(data.Amount), "Amount is below minimum monthly contribution.", data.Amount));

            if (customer?.MemberType == MemberType.RETIREE && data.Amount < 5000)
                context.AddFailure(new ValidationFailure(nameof(data.Amount), "Amount is below minimum monthly contribution.", data.Amount));

            
        });


    }


}



