using AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationSchedules;

public class DeleteLoanApplicationScheduleCommandValidator : AbstractValidator<DeleteLoanApplicationScheduleCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<DeleteLoanApplicationScheduleCommandValidator> logger;

  public DeleteLoanApplicationScheduleCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<DeleteLoanApplicationScheduleCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanApplicationSchedules.Where(r => r.Id == data.Id).Any();
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));

      /*
      var checkChild = dbContext.ChildTable.Where(r => r.ChildTableId == data.Id).Any();
      if (checkChild)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.Id),
          "Selected record has dependent records and cannot be deleted", data.Id));

      }
      */
    });
  }
}