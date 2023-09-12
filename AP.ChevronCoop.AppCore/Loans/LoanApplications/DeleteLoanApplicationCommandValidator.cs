using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplications;

public class DeleteLoanApplicationCommandValidator : AbstractValidator<DeleteLoanApplicationCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<DeleteLoanApplicationCommandValidator> logger;

  public DeleteLoanApplicationCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<DeleteLoanApplicationCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Id is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanApplications.Any(r => r.Id == data.Id);
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));
    });
  }
}