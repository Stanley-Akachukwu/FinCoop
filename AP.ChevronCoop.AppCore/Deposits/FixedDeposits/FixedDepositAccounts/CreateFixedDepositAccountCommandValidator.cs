using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace ChevronCoop.API.Controllers.Deposits.FixedDeposits.FixedDepositAccounts;

public partial class CreateFixedDepositAccountCommandValidator : AbstractValidator<CreateFixedDepositAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateFixedDepositAccountCommandValidator> logger;
    public CreateFixedDepositAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateFixedDepositAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.CustomerId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.DepositProductId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.Amount).NotNull();
        RuleFor(p => p.InterestRate).NotNull();

        RuleFor(p => p.TenureUnit).NotEmpty();
        RuleFor(p => p.TenureValue).NotNull();


        RuleFor(p => p.LiquidationAccountType).NotEmpty().WithMessage("Liquidation Account Type is required.");
        RuleFor(p => p.LiquidationAccountId).NotEmpty().WithMessage("Liquidation Account is required");

    

        RuleFor(p => p).Custom((data, context) =>
        {

            var application = dbContext.Customers.Where(r => r.Id == data.ApplicationId).FirstOrDefault();

            if (application == null)
                context.AddFailure(
                     new ValidationFailure(nameof(data.ApplicationId),
                         "Application not found.", data.ApplicationId));


            var customer = dbContext.Customers.Where(r => r.Id == data.CustomerId).FirstOrDefault();

            if (customer == null)
                context.AddFailure(
                     new ValidationFailure(nameof(data.CustomerId),
                         "Customer not found.", data.CustomerId));



            var depositProduct = dbContext.DepositProducts.Where(r => r.Id == data.DepositProductId).FirstOrDefault();

            if (depositProduct == null)
                context.AddFailure(
                     new ValidationFailure(nameof(data.DepositProductId),
                         "Deposit Product not found.", data.DepositProductId));



        });

    }


}