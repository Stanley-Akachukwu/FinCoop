using AP.ChevronCoop.AppDomain.Loans.LoanApplicationApprovals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationApprovals;

public class CreateLoanApplicationApprovalCommandValidator : AbstractValidator<CreateLoanApplicationApprovalCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<CreateLoanApplicationApprovalCommandValidator> logger;

  public CreateLoanApplicationApprovalCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<CreateLoanApplicationApprovalCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(p => p.Status).NotEmpty().MaximumLength(64);
    RuleFor(p => p.LoanApplicationId).NotEmpty().MaximumLength(80);
    RuleFor(p => p).Custom((data, context) =>
    {
      /*
      var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
      if (!parentExists)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.ParentId),
          "Invalid key.", data.ParentId));

      }

      var checkName = dbContext.LoanApplicationApprovals.Where(r => r.Name.ToLower() == data.Name.ToLower() 
      && r.CodeTypeId == data.CodeTypeId).Any();
      if (checkName)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.Name),
          "Duplicate names are not allowed.", data.Name));
      }

      var checkCode = dbContext.LoanApplicationApprovals.Where(r => r.Code.ToLower() == data.Code.ToLower() 
      && r.CodeTypeId != data.CodeTypeId).Any();
      if (checkCode)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.Code),
          "Duplicate codes are not allowed.", data.Code));
      }
      */
    });
  }
}