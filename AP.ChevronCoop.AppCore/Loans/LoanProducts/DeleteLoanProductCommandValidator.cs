using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanProducts;

public class DeleteLoanProductCommandValidator : AbstractValidator<DeleteLoanProductCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<DeleteLoanProductCommandValidator> logger;

  public DeleteLoanProductCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<DeleteLoanProductCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Id is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanProducts.Any(r => r.Id == data.Id);
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));
    });
  }
}