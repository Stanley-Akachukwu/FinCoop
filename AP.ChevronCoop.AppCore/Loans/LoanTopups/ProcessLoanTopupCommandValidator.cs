using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopups
{
    public class ProcessLoanTopupCommandValidator : AbstractValidator<ProcessLoanTopupCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<ProcessLoanTopupCommandValidator> logger;
        public ProcessLoanTopupCommandValidator(ChevronCoopDbContext appDbContext, ILogger<ProcessLoanTopupCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.LoanTopupId).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Status).IsInEnum();

            RuleFor(p => p).Custom((data, context) =>
            {
                var loanTopup = dbContext.LoanTopups
                    .Include(x => x.LoanAccount)
                    .FirstOrDefault(r => r.Id == data.LoanTopupId && r.IsActive);

                if (loanTopup == null)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanTopupId),
                    "Loan topup does not exist.", data.LoanTopupId));
                }

                var isExist = dbContext.LoanAccounts.Any(r => r.Id == loanTopup!.LoanAccountId && r.IsActive);
                if (!isExist)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(loanTopup.LoanAccountId),
                    "Loan account does not exist.", loanTopup!.LoanAccountId));
                }

                isExist = dbContext.LoanApplications.Any(r => r.Id == loanTopup!.LoanAccount.LoanApplicationId && r.IsActive);
                if (!isExist)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(loanTopup.LoanAccount.LoanApplicationId),
                    "Loan application does not exist.", loanTopup!.LoanAccount.LoanApplicationId));
                }

                var approval = dbContext.Approvals.FirstOrDefault(x => x.Id == loanTopup!.ApprovalId);
                if (approval == null)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(loanTopup.Approval),
                    "Loan topup is pending approval.", loanTopup!.Approval));
                }

                if (approval.Status == ApprovalStatus.APPROVED)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(loanTopup.Approval),
                    "Loan topup has already been approved.", loanTopup!.Approval));
                }
            });
        }
    }
}
