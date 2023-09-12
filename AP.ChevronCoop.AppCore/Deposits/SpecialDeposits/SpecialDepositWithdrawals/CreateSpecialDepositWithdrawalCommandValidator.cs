using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public partial class CreateSpecialDepositWithdrawalCommandValidator : AbstractValidator<CreateSpecialDepositWithdrawalCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateSpecialDepositWithdrawalCommandValidator> logger;
        public CreateSpecialDepositWithdrawalCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSpecialDepositWithdrawalCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(x => x.Amount).NotEmpty().Must(x => x > 1000).WithMessage("Amount is required and must be greater than  N1,000.00.");
            RuleFor(x => x.WithdrawalDestinationType).IsInEnum().WithMessage("Invalid destination account.");
            RuleFor(p => p.CustomerDestinationBankAccountId).NotNull().WithMessage("Invalid destination account.");
            RuleFor(x => x.CreatedByUserId).NotEmpty().WithMessage("CreatedByUserId is required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var sdAccount = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.SpecialDepositSourceAccountId).Include(a => a.DepositAccount).FirstOrDefault();
                if (sdAccount == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(data.SpecialDepositSourceAccountId),"Selected account does not exist", data.SpecialDepositSourceAccountId));
                }
                if (sdAccount.DepositAccount.LedgerBalance < data.Amount)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Amount),
                    "Insufficient fund for withdrawal", data.Amount));
                }

                var customerBankAccount = dbContext.CustomerBankAccounts.FirstOrDefault(p =>  p.Id == data.CustomerDestinationBankAccountId);
                if(customerBankAccount == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(data.CustomerDestinationBankAccountId), "Invalid Customer Withdrawal Destination Bank Account", data.CustomerDestinationBankAccountId));
                }

                LedgerAccount CUST_LEDGER_BANK_ACC = dbContext.LedgerAccounts.FirstOrDefault(c => c.Id == customerBankAccount.LedgerAccountId);

                if (CUST_LEDGER_BANK_ACC == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(data.CustomerDestinationBankAccountId), "Invalid Ledger Account for Customer Withdrawal Destination Bank Account Specified", data.CustomerDestinationBankAccountId));
                }

            });

        }


    }
}



