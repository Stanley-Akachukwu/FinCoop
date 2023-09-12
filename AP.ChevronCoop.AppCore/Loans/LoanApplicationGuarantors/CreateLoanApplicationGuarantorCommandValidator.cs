using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationGuarantors;

public class CreateLoanApplicationGuarantorCommandValidator : AbstractValidator<CreateLoanApplicationGuarantorCommand>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateLoanApplicationGuarantorCommandValidator> logger;

    public CreateLoanApplicationGuarantorCommandValidator(ChevronCoopDbContext appDbContext,
      ILogger<CreateLoanApplicationGuarantorCommandValidator> _logger)
    {
        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.GuarantorType).IsInEnum();

        RuleFor(x => x.GuarantorApprovalType).IsInEnum();

        RuleFor(x => x.GuarantorProfileId)
      .NotEmpty().WithMessage("Guarantor profile id is required.");

        RuleFor(x => x.LoanApplicationId)
          .NotEmpty().WithMessage("Loan application id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {
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