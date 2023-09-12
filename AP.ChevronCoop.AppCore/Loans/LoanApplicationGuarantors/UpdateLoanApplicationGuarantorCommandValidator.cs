using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationGuarantors;

public class UpdateLoanApplicationGuarantorCommandValidator : AbstractValidator<UpdateLoanApplicationGuarantorCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdateLoanApplicationGuarantorCommandValidator> logger;

  public UpdateLoanApplicationGuarantorCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<UpdateLoanApplicationGuarantorCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(p => p.Id).NotEmpty();

    RuleFor(x => x.GuarantorType)
      .NotNull().WithMessage("Guarantor type is required.");

    RuleFor(x => x.GuarantorProfileId)
      .NotNull().WithMessage("Guarantor profile id is required.");

    RuleFor(x => x.LoanApplicationId)
      .NotNull().WithMessage("Loan application id is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanApplicationGuarantors.Where(r => r.Id == data.Id).Any();
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));

      var isExist = dbContext.MemberProfiles.Where(x => x.MembershipId == data.GuarantorProfileId).Any();
      if (!isExist)
        context.AddFailure(
          new ValidationFailure(nameof(data.GuarantorProfileId),
            "Guarantor profile does not exist.", data.GuarantorProfileId));

      isExist = dbContext.LoanApplications.Where(x => x.Id == data.LoanApplicationId).Any();
      if (!isExist)
        context.AddFailure(
          new ValidationFailure(nameof(data.LoanApplicationId),
            "Loan appication does not exist.", data.LoanApplicationId));
    });
  }
}