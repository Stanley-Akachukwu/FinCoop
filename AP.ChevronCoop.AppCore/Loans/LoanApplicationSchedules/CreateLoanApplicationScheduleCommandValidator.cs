using AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationSchedules;

public class CreateLoanApplicationScheduleCommandValidator : AbstractValidator<CreateLoanApplicationScheduleCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<CreateLoanApplicationScheduleCommandValidator> logger;

  public CreateLoanApplicationScheduleCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<CreateLoanApplicationScheduleCommandValidator> _logger)
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

    RuleFor(p => p).Custom((data, context) => { });
  }
}