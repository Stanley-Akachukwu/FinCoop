using AP.ChevronCoop.AppDomain.Loans.LoanApplicationApprovals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationApprovals;

public class DeleteLoanApplicationApprovalCommandValidator : AbstractValidator<DeleteLoanApplicationApprovalCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<DeleteLoanApplicationApprovalCommandValidator> logger;

  public DeleteLoanApplicationApprovalCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<DeleteLoanApplicationApprovalCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanApplicationApprovals.Where(r => r.Id == data.Id).Any();
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