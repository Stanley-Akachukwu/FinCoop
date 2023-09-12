using AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationSchedules;

public class UpdateLoanApplicationScheduleCommandValidator : AbstractValidator<UpdateLoanApplicationScheduleCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdateLoanApplicationScheduleCommandValidator> logger;

  public UpdateLoanApplicationScheduleCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<UpdateLoanApplicationScheduleCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(p => p.Id).NotEmpty();
    RuleFor(p => p.LoanApplicationId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.RepaymentNo).NotNull();
    RuleFor(p => p.RepaymentDate).NotNull();
    RuleFor(p => p.TenureUnit).NotEmpty().MaximumLength(64);
    RuleFor(p => p.PeriodPayment).NotNull();
    RuleFor(p => p.PeriodPrincipal).NotNull();
    RuleFor(p => p.PeriodInterest).NotNull();

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanApplicationSchedules.Where(r => r.Id == data.Id).Any();
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));

      /*
      var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
      if (!parentExists)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.ParentId),
          "Invalid key.", data.ParentId));

      }

      var checkName = dbContext.LoanApplicationSchedules.Where(r => r.Id != data.Id &&
      r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
      if (checkName)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.Name),
          "Duplicate names are not allowed.", data.Name));
      }

      var checkCode = dbContext.LoanApplicationSchedules.Where(r => r.Id != data.Id &&
      r.Code.ToLower() == data.Code.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
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