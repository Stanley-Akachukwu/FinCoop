using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffSets
{
    public partial class CreateLoanOffsetCommandValidator : AbstractValidator<CreateLoanOffsetCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateLoanOffsetCommandValidator> logger;
        public CreateLoanOffsetCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateLoanOffsetCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.LoanAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.OffsetAmount).NotNull().GreaterThan(0);
            RuleFor(p => p.SpecialDepositAccountId).NotEmpty()
                .When(x => x.LoanRepaymentMode == LoanRepaymentMode.SPECIAL_DEPOSIT).MaximumLength(80);

            RuleFor(p => p.SavingsAccountId).NotEmpty()
                .When(x => x.LoanRepaymentMode == LoanRepaymentMode.SAVINGS).MaximumLength(80);

            RuleFor(p => p.CustomerPaymentDocumentId).NotEmpty()
                .When(x => x.LoanRepaymentMode == LoanRepaymentMode.BANK_TRANSFER).MaximumLength(80);

            RuleFor(p => p.PrincipalBalance).NotNull();
            RuleFor(p => p.InterestBalance).NotNull();
            // RuleFor(p => p.TotalOffsetCharges).NotNull();
            RuleFor(p => p.LoanRepaymentMode).IsInEnum();
            RuleFor(p => p.OffSetRepaymentDate).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {
                var loanAccount = dbContext.LoanAccounts
                        .Include(x => x.PrincipalBalanceAccount)
                        .FirstOrDefault(c => c.Id == data.LoanAccountId);

                if (loanAccount is null)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanAccountId),
                    "Loan account does not exist.", data.LoanAccountId));
                }

                if (loanAccount != null && data.OffsetAmount > loanAccount!.PrincipalBalanceAccount.LedgerBalance)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.OffsetAmount),
                    "The offset amount is greater than the allowed amount.", data.OffsetAmount));
                }
                
                // Check if loan has been disbursed
                var isDisbursed = dbContext.LoanDisbursements.Any(x => x.LoanAccountId == data.LoanAccountId && x.Status == TransactionStatus.SUCCESS);
                if (loanAccount != null && !isDisbursed)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanAccountId),
                    "Loan has not been disbursed.", data.LoanAccountId));
                }

                if (data.LoanRepaymentMode == LoanRepaymentMode.SPECIAL_DEPOSIT)
                {
                    var specialDepositAccount = dbContext.SpecialDepositAccounts.Include(x => x.DepositAccount)
                        .FirstOrDefault(r => r.Id == data.SpecialDepositAccountId && r.IsActive);

                    if (specialDepositAccount is null || specialDepositAccount.DepositAccount is null)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.SpecialDepositAccountId),
                        "Special deposit account not found.", data.SpecialDepositAccountId));
                    }

                    if (specialDepositAccount!.DepositAccount is not null && specialDepositAccount.DepositAccount.LedgerBalance < data.OffsetAmount)
                    {
                        context.AddFailure(
                            new ValidationFailure(nameof(data.SavingsAccountId),
                            "Customer savings account balance is lower than the offset amount", data.SpecialDepositAccountId));
                    }
                }

                if (data.LoanRepaymentMode == LoanRepaymentMode.SAVINGS)
                {
                    var savingsAccount = dbContext.SavingsAccounts.Include(x => x.LedgerDepositAccount)
                        .FirstOrDefault(r => r.Id == data.SavingsAccountId && r.IsActive);

                    if (savingsAccount is null || savingsAccount.LedgerDepositAccount is null)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.SavingsAccountId),
                        "Savings account not found.", data.SavingsAccountId));
                    }

                    if (savingsAccount?.LedgerDepositAccount is not null && savingsAccount?.LedgerDepositAccount.LedgerBalance < data.OffsetAmount)
                    {
                        context.AddFailure(
                            new ValidationFailure(nameof(data.SavingsAccountId),
                            "Customer savings account balance is lower than the offset amount", data.SavingsAccountId));
                    }
                }

                if (data.LoanRepaymentMode == LoanRepaymentMode.BANK_TRANSFER)
                {
                    var isExist = dbContext.CustomerPaymentDocuments.Any(r => r.Id == data.CustomerPaymentDocumentId && r.IsActive);
                    if (!isExist)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.CustomerPaymentDocumentId),
                        "Customer payment document does not exist.", data.CustomerPaymentDocumentId));
                    }
                }
            });
        }
    }
}