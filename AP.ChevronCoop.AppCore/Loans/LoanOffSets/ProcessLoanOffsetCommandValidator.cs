using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffSets
{
    public class ProcessLoanOffsetCommandValidator : AbstractValidator<ProcessLoanOffsetCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<ProcessLoanOffsetCommandValidator> logger;
        public ProcessLoanOffsetCommandValidator(ChevronCoopDbContext appDbContext, ILogger<ProcessLoanOffsetCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.LoanOffsetId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.Status).IsInEnum();

            RuleFor(p => p).Custom((data, context) =>
            {
                var offset = dbContext.LoanOffsets.Include(x => x.LoanAccount)
                .FirstOrDefault(r => r.Id == data.LoanOffsetId && r.IsActive);
                if (offset is null)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanOffsetId),
                    "Loan offset does not exist.", data.LoanOffsetId));
                }

                if (offset!.LoanAccount is null)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanOffsetId),
                    "Loan offset does not have a loan account.", data.LoanOffsetId));
                }

                if (offset!.LoanAccount is not null && offset.LoanAccount.IsClosed)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanOffsetId),
                    "Loan offset's account is already closed.", data.LoanOffsetId));
                }
            });
        }
    }
}
