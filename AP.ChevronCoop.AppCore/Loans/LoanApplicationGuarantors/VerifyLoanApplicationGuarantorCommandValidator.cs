using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationGuarantors;

public class VerifyLoanApplicationGuarantorCommandValidator : AbstractValidator<VerifyLoanApplicationGuarantorCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<VerifyLoanApplicationGuarantorCommandValidator> logger;

  public VerifyLoanApplicationGuarantorCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<VerifyLoanApplicationGuarantorCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(x => x.MembershipId)
      .NotEmpty().WithMessage("Membership Id is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      var entity = dbContext.Customers.FirstOrDefault(x => x.MemberId == data.MembershipId);
      if (entity is null)
      {
        context.AddFailure(
          new ValidationFailure(nameof(data.MembershipId),
            "Invalid Membership Id, the provided Membership Id is not a customer.", data.MembershipId));
      }

      if (entity?.DateOfEmployment is null)
      {
        context.AddFailure(
          new ValidationFailure(nameof(data.MembershipId),
            "Guarantor date of employment is null", data.MembershipId));
      }
      
      if (entity?.DOB is null)
      {
        context.AddFailure(
          new ValidationFailure(nameof(data.MembershipId),
            "Guarantor date of birth is null", data.MembershipId));
      }

      if (entity?.DateOfEmployment is not null && entity?.DOB is not null)
      {
        var dateOfEmployment = entity.DateOfEmployment;
        var dateDiff = DateTime.Now - dateOfEmployment;
        var months = dateDiff.Value.Days / (365.25 / 12);
        if (months < 6)
        {
          context.AddFailure(
            new ValidationFailure(nameof(data.MembershipId),
              "Guarantor is less than 6 months in service", data.MembershipId));
        }
        
        // verify guarantor retirement status
        var retirementByAge = entity.DOB.Value.AddYears(60);
        var retirementByWork = entity.DateOfEmployment.Value.AddYears(35);

        var retirementValue = retirementByAge < retirementByWork ? retirementByAge : retirementByWork;
        var loanPeriod = data.CommencementDate.AddMonths(data.TenureInMonths);

        if (retirementValue < loanPeriod)
        {
          context.AddFailure(
            new ValidationFailure(nameof(data.MembershipId),
              "Guarantor is due for retirement before the loan tenure", data.MembershipId));
        }
      }
      
    });
  }
}