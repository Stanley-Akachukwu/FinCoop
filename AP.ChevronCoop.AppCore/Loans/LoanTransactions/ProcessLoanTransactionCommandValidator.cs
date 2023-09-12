using AP.ChevronCoop.AppDomain.Loans.LoanTransactions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanTransactions
{
    public partial class ProcessLoanTransactionCommandValidator : AbstractValidator<LoanTransactionCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<ProcessLoanTransactionCommandValidator> logger;
        public ProcessLoanTransactionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<ProcessLoanTransactionCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.LoanAccountId).NotEmpty().MaximumLength(80);
            

            RuleFor(p => p).Custom((data, context) =>
            {
                var loanAccount = dbContext.LoanAccounts.Any(r => r.Id == data.LoanAccountId && r.IsActive);
                if (!loanAccount)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanAccountId),
                    "Loan account does not exist.", data.LoanAccountId));
                }

            });
        }
    }
}
