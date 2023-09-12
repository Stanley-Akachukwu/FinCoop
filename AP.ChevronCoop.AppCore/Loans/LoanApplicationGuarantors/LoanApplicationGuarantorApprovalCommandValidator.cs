using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationGuarantors;

public class LoanApplicationGuarantorApprovalCommandValidator : AbstractValidator<LoanApplicationGuarantorApprovalCommand>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<LoanApplicationGuarantorApprovalCommandValidator> logger;

    public LoanApplicationGuarantorApprovalCommandValidator(ChevronCoopDbContext appDbContext,
      ILogger<LoanApplicationGuarantorApprovalCommandValidator> _logger)
    {
        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.LoanApplicationId)
          .NotEmpty().WithMessage("Loan application id is required.");

        RuleFor(x => x.GuarantorId)
          .NotEmpty().WithMessage("Guarantor id is required.");

        RuleFor(x => x.IsApproved)
          .NotNull().WithMessage("Approval Status is required.");

        RuleFor(p => p).Custom((data, context) =>
        {
            var isExist = dbContext.LoanApplications.Any(x => x.Id == data.LoanApplicationId);
            if (!isExist)
                context.AddFailure(
                  new ValidationFailure(nameof(data.LoanApplicationId),
                    "Loan application does not exist.", data.LoanApplicationId));

            isExist = dbContext.Customers.Any(x => x.Id == data.GuarantorId);
            if (!isExist)
                context.AddFailure(
                  new ValidationFailure(nameof(data.GuarantorId),
                    "Guarantor does not exist.", data.GuarantorId));
        });
    }
}