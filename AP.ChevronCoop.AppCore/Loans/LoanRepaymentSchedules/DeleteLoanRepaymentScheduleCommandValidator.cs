using AP.ChevronCoop.AppDomain.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepaymentSchedules;

public class DeleteLoanRepaymentScheduleCommandValidator : AbstractValidator<DeleteLoanRepaymentScheduleCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<DeleteLoanRepaymentScheduleCommandValidator> logger;

  public DeleteLoanRepaymentScheduleCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<DeleteLoanRepaymentScheduleCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Id is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      // var checkId = dbContext.LoanRepaymentSchedules.Any(r => r.Id == data.Id);
      // if (!checkId)
      //     context.AddFailure(
      //   new ValidationFailure(nameof(data.Id),
      //     "Selected Id does not exist", data.Id));
    });
  }
}