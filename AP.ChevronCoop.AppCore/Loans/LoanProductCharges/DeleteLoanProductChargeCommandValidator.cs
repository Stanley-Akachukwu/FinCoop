using AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanProductCharges;

public class DeleteLoanProductChargeCommandValidator : AbstractValidator<DeleteLoanProductChargeCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<DeleteLoanProductChargeCommandValidator> logger;

  public DeleteLoanProductChargeCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<DeleteLoanProductChargeCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Id is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanProductCharges.Any(r => r.Id == data.Id);
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));
    });
  }
}