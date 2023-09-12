using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopups
{
    public partial class CreateLoanTopupCommandValidator : AbstractValidator<CreateLoanTopupCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateLoanTopupCommandValidator> logger;
        public CreateLoanTopupCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateLoanTopupCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.LoanAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.TopupAmount).NotNull().GreaterThan(0);
            RuleFor(p => p.DestinationType).IsInEnum();
            RuleFor(p => p.SpecialDepositAccountId).NotEmpty()
                .When(x => x.DestinationType == TopupFundingSourceType.SPECIAL_DEPOSIT_ACCOUNT).MaximumLength(80);

            RuleFor(p => p.CustomerBankAccountId).NotEmpty()
                .When(x => x.DestinationType == TopupFundingSourceType.EXISTING_BANK_ACCOUNT).MaximumLength(80);
            ;
            RuleFor(p => p.TopupDate).NotNull();
            RuleFor(p => p.CommencementDate).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {
                var loanAccount = dbContext.LoanAccounts
                    .Include(x => x.LoanApplication.LoanProduct)
                    .FirstOrDefault(r => r.Id == data.LoanAccountId && r.IsActive);

                var topupPendingApproval = dbContext.LoanTopups.Include(x => x.Approval).Any(x => x.LoanAccountId == data.LoanAccountId && x.Approval!.Status != ApprovalStatus.APPROVED);
                if (topupPendingApproval)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanAccountId),
                    "You have a pending topup approval.", data.LoanAccountId));
                }

                if (loanAccount == null)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.LoanAccountId),
                    "Loan account does not exist.", data.LoanAccountId));
                }

                if (data.DestinationType == TopupFundingSourceType.SPECIAL_DEPOSIT_ACCOUNT)
                {
                    var isExist = dbContext.SpecialDepositAccounts.Any(r => r.Id == data.SpecialDepositAccountId && r.IsActive);
                    if (!isExist)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.SpecialDepositAccountId),
                        "Special deposit account not found.", data.SpecialDepositAccountId));
                    }
                }
                
                // Check if loan has been disbursed
                var isDisbursed = dbContext.LoanDisbursements.Any(x => x.LoanAccountId == data.LoanAccountId && x.Status == TransactionStatus.SUCCESS);
                if (loanAccount != null && !isDisbursed)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.LoanAccountId),
                            "Loan has not been disbursed.", data.LoanAccountId));
                }

                if (data.DestinationType == TopupFundingSourceType.EXISTING_BANK_ACCOUNT)
                {
                    var isExist = dbContext.CustomerBankAccounts.Any(r => r.Id == data.CustomerBankAccountId && r.IsActive);
                    if (!isExist)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.CustomerBankAccountId),
                        "Customer bank account not found.", data.CustomerBankAccountId));
                    }
                }

                if (data.Guarantors != null && data.Guarantors.Any())
                {
                    if (data.CreatedByUserId != null && data.Guarantors.Exists(x => x.GuarantorCustomerId == data.CreatedByUserId))
                    {
                        context.AddFailure(
                                new ValidationFailure(nameof(data.Guarantors),
                                    "You cannot select yourself as a guarantor", data.Guarantors));
                    }

                    var totalRetiree = data.Guarantors.Count(x => x.GuarantorType == GuarantorType.RETIREE);
                    var totalRegular = data.Guarantors.Count(x => x.GuarantorType == GuarantorType.REGULAR);

                    if (totalRetiree > loanAccount!.LoanApplication!.LoanProduct!.NonEmployeeGuarantorCount)
                    {
                        context.AddFailure(
                                new ValidationFailure(nameof(data.Guarantors),
                                    "Total retiree guarantor is more than the configured guarantor(s) in loan product.", data.Guarantors));
                    }
                    if (totalRegular > loanAccount!.LoanApplication!.LoanProduct!.EmployeeGuarantorCount)
                    {
                        context.AddFailure(
                                new ValidationFailure(nameof(data.Guarantors),
                                    "Total employee guarantor is more than the configured guarantor(s) in loan product.", data.Guarantors));
                    }

                    var guarantorIds = data.Guarantors.Select(x => x.GuarantorCustomerId);
                    foreach (var guarantor in guarantorIds)
                    {
                        var guarantorsProfile = dbContext.Customers.Any(x => x.Id == guarantor);
                        if (!guarantorsProfile)
                        {
                            context.AddFailure(
                                new ValidationFailure(nameof(data.Guarantors),
                                    "Guarantor profile does not exist.", data.Guarantors));
                        }
                    }

                }
            });
        }
    }
}
