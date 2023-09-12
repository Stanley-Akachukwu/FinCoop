using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositTransactions
{
    public partial class ProcessDepositTransactionCommandValidator : AbstractValidator<DepositTransactionCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<ProcessDepositTransactionCommandValidator> logger;
        public ProcessDepositTransactionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<ProcessDepositTransactionCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.DepositAccountId).NotEmpty().MaximumLength(80);
            

            RuleFor(p => p).Custom((data, context) =>
            {
                // var depositAccount = dbContext.DepositAccounts.Any(r => r.Id == data.DepositAccountId && r.IsActive);
                // if (!depositAccount)
                // {
                //     context.AddFailure(
                //     new ValidationFailure(nameof(data.DepositAccountId),
                //     "Deposit account does not exist.", data.DepositAccountId));
                // }

            });
        }
    }
}
