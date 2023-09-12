using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationGuarantors
{
    public class LoanTopupGuarantorApprovalCommandValidator : AbstractValidator<LoanTopupGuarantorApprovalCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<LoanTopupGuarantorApprovalCommandValidator> logger;

        public LoanTopupGuarantorApprovalCommandValidator(ChevronCoopDbContext appDbContext,
          ILogger<LoanTopupGuarantorApprovalCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(x => x.LoanAccountId)
              .NotEmpty().WithMessage("Loan account id is required.");

            RuleFor(x => x.GuarantorId)
              .NotEmpty().WithMessage("Guarantor id is required.");

            RuleFor(x => x.IsApproved)
              .NotNull().WithMessage("Approval Status is required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var isExist = dbContext.LoanAccounts.Any(x => x.Id == data.LoanAccountId);
                if (!isExist)
                    context.AddFailure(
                      new ValidationFailure(nameof(data.LoanAccountId),
                        "Loan account does not exist.", data.LoanAccountId));

                isExist = dbContext.Customers.Any(x => x.Id == data.GuarantorId);
                if (!isExist)
                    context.AddFailure(
                      new ValidationFailure(nameof(data.GuarantorId),
                        "Guarantor does not exist.", data.GuarantorId));
            });
        }
    }
}
