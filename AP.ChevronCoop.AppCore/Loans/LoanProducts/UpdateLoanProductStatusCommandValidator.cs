using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanProducts;

public class UpdateLoanProductStatusCommandValidator : AbstractValidator<UpdateLoanProductStatusCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdateLoanProductStatusCommandValidator> logger;

  public UpdateLoanProductStatusCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<UpdateLoanProductStatusCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(p => p.Id).NotEmpty();

    RuleFor(p => p.Status).IsInEnum();

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