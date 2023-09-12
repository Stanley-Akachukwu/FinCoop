using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace AP.ChevronCoop.AppCore.Loans.LoanProducts
{
    public class CustomerLoanProductEligibilityCommandValidator : AbstractValidator<CustomerLoanProductEligibilityCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger<CustomerLoanProductEligibilityCommandValidator> _logger;

        public CustomerLoanProductEligibilityCommandValidator(
            ChevronCoopDbContext dbContext, 
            ILogger<CustomerLoanProductEligibilityCommandValidator> logger)
        {
            _dbContext = dbContext;
            _logger = logger;

            RuleFor(p => p.CustomerId)
              .NotEmpty().WithMessage("Customer Id is required.");

            RuleFor(p => p.LoanProductId)
              .NotEmpty().WithMessage("Loan Product Id is required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var isExist = _dbContext.LoanProducts.Any(x => x.Id == data.LoanProductId);
                if (!isExist)
                    context.AddFailure(
                      new ValidationFailure(nameof(data.LoanProductId),
                        "Loan product does not exist.", data.LoanProductId));

                isExist = _dbContext.Customers.Any(x => x.Id == data.CustomerId);
                if (!isExist)
                    context.AddFailure(
                      new ValidationFailure(nameof(data.CustomerId),
                        "Customer does not exist.", data.CustomerId));
            });
        }
    }
}
