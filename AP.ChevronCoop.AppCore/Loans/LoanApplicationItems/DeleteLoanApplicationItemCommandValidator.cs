using AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationItems;

public class DeleteLoanApplicationItemCommandValidator : AbstractValidator<DeleteLoanApplicationItemCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<DeleteLoanApplicationItemCommandValidator> logger;

  public DeleteLoanApplicationItemCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<DeleteLoanApplicationItemCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Id is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanApplicationItems.Any(r => r.Id == data.Id);
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));
    });
  }
}