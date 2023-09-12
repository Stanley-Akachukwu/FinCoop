using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanProducts;

public class PublishLoanProductCommandValidator: AbstractValidator<PublishLoanProductCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<PublishLoanProductCommandValidator> logger;

  public PublishLoanProductCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<PublishLoanProductCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(p => p.PublicationType).IsInEnum();
    RuleFor(p => p.ProductId).NotEmpty().MaximumLength(40);

    RuleFor(p => p).Custom((data, context) =>
    {
      var exist = dbContext.LoanProducts.Any(r => r.Id == data.ProductId);
      if (!exist)
      {
        context.AddFailure(
          new ValidationFailure(nameof(data.ProductId),
            "Loan product does not exist.", data.ProductId));
      }

      if (data.PublicationType == PublicationType.DEPARTMENT)
      {
        exist = dbContext.Departments.Any(r => data.Targets.Contains(r.Id));
        if (!exist)
        {
          context.AddFailure(
            new ValidationFailure(nameof(data.Targets),
              "Department does not not exist.", data.Targets));
        }
      }

      if (data.PublicationType == PublicationType.CUSTOMER)
      {
        exist = dbContext.Customers.Any(r => data.Targets.Contains(r.Id));
        if (!exist)
        {
          context.AddFailure(
            new ValidationFailure(nameof(data.Targets),
              "Customer does not not exist.", data.Targets));
        }
      }
    });
  }
}