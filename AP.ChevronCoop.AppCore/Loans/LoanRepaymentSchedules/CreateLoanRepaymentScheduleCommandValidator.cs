using AP.ChevronCoop.AppDomain.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepaymentSchedules;

public class CreateLoanRepaymentScheduleCommandValidator : AbstractValidator<CreateLoanRepaymentScheduleCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<CreateLoanRepaymentScheduleCommandValidator> logger;

  public CreateLoanRepaymentScheduleCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<CreateLoanRepaymentScheduleCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(x => x.TenureUnit)
      .NotEmpty().WithMessage("Tenure unit is required.");

    RuleFor(x => x.TenureValue)
      .NotEmpty().WithMessage("Tenure value is required.")
      .GreaterThan(0);

    RuleFor(x => x.Amount)
      .NotEmpty().WithMessage("Amount is required.")
      .GreaterThan(0);

    RuleFor(x => x.Interest)
      .NotEmpty().WithMessage("Interest rate is required.")
      .GreaterThan(0);

    RuleFor(x => x.CommencementDate).NotEmpty()
      .Must(date => date != default)
      .WithMessage("Commencement date is required.");

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

              var checkName = dbContext.LoanRepaymentSchedules.Where(r => r.Name.ToLower() == data.Name.ToLower()
      && r.CodeTypeId == data.CodeTypeId).Any();
              if (checkName)
              {
                  context.AddFailure(
                  new ValidationFailure(nameof(data.Name),
                  "Duplicate names are not allowed.", data.Name));
              }

              var checkCode = dbContext.LoanRepaymentSchedules.Where(r => r.Code.ToLower() == data.Code.ToLower()
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