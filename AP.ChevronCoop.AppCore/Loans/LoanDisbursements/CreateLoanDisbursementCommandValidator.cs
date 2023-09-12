using AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanDisbursements
{
    public partial class CreateLoanDisbursementCommandValidator : AbstractValidator<CreateLoanDisbursementCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateLoanDisbursementCommandValidator> logger;

        public CreateLoanDisbursementCommandValidator(
            ChevronCoopDbContext appDbContext,
            ILogger<CreateLoanDisbursementCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            // RuleFor(p => p.Status).NotEmpty().MaximumLength(64);
            RuleFor(p => p.LoanAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.Amount).NotNull();
            
            RuleFor(m => m.DisbursementMode).NotEmpty().WithMessage("Disbursement Mode is required")
                .Must(s => s.Equals(LoanDisbursementMode.BANK_TRANSFER) || s.Equals(LoanDisbursementMode.SPECIAL_DEPOSIT))
                .WithMessage("Invalid Disbursement Mode");

            RuleFor(p => p).Custom((data, context) =>
            {
                var check = dbContext.LoanDisbursements.Any(x => x.LoanAccountId == data.LoanAccountId);
                if (check)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.LoanAccountId),
                            "A disbursement has been initiated for this application.", data.LoanAccountId));
                }
                
                var loanAccount = dbContext.LoanAccounts.FirstOrDefault(x => x.Id == data.LoanAccountId);
                if (loanAccount == null)
                    context.AddFailure(
                        new ValidationFailure(nameof(data.LoanAccountId),
                            "Invalid Loan account Id.", data.LoanAccountId));
                
                if (data.Amount < loanAccount.Principal)
                    context.AddFailure(
                        new ValidationFailure(nameof(data.LoanAccountId),
                            "Partial disbursement not supported at the moment.", data.LoanAccountId));

            });
        }
    }
}