using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplications;

public class UpdateLoanApplicationCommandValidator : AbstractValidator<UpdateLoanApplicationCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdateLoanApplicationCommandValidator> logger;

  public UpdateLoanApplicationCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<UpdateLoanApplicationCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(p => p.Id).NotEmpty();

    RuleFor(p => p.LoanProductId)
      .NotEmpty().WithMessage("Loan product id is required.");

    RuleFor(p => p.MemberProfileId)
      .NotEmpty().WithMessage("Member profile id is required.");

    RuleFor(p => p.Amount).NotEmpty().WithMessage("Amount is required.")
      .GreaterThan(0);

    RuleFor(p => p.RepaymentPeriod).NotEmpty().WithMessage("Loan repayment period is required.")
      .GreaterThan(0);

    RuleFor(p => p.RepaymentTenureUnit)
      .NotEmpty().WithMessage("Repayment tenure unit is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanApplications.Where(r => r.Id == data.Id).Any();
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));
    });
  }
}