using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplications;

public class LoanApplicationEligibilityCommandValidator : AbstractValidator<LoanApplicationEligibilityCommand>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateLoanApplicationCommandValidator> logger;

    public LoanApplicationEligibilityCommandValidator(ChevronCoopDbContext appDbContext,
      ILogger<CreateLoanApplicationCommandValidator> _logger)
    {
        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.LoanProductId)
          .NotEmpty().WithMessage("Loan product id is required.");

        RuleFor(x => x.CustomerId)
          .NotEmpty().WithMessage("Customer profile id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {
            var loanProduct = dbContext.LoanProducts.Any(x => x.Id == data.LoanProductId);
            if (!loanProduct)
                context.AddFailure(
                  new ValidationFailure(nameof(data.LoanProductId),
                    "Loan product does not exist.", data.LoanProductId));

            var isCustomerExist = dbContext.Customers.Any(x => x.Id == data.CustomerId);
            if (!isCustomerExist)
                context.AddFailure(
                  new ValidationFailure(nameof(data.CustomerId),
                    "Customer profile does not exist.", data.CustomerId));
            
        });
    }
}