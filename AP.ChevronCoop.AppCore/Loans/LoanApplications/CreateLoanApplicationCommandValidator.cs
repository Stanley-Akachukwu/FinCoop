using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplications;

public class CreateLoanApplicationCommandValidator : AbstractValidator<CreateLoanApplicationCommand>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateLoanApplicationCommandValidator> logger;

    public CreateLoanApplicationCommandValidator(ChevronCoopDbContext appDbContext,
      ILogger<CreateLoanApplicationCommandValidator> _logger)
    {
        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.LoanProductId)
          .NotEmpty().WithMessage("Loan product id is required.");

        RuleFor(x => x.CustomerId)
          .NotEmpty().WithMessage("Customer profile id is required.");

        RuleFor(x => x.Amount).NotEmpty()
          .GreaterThan(0).WithMessage("Amount should be greater than 0.");

        RuleFor(x => x.TenureValue).NotEmpty()
            .GreaterThan(0).WithMessage("Tenure value should be greater than 0.");

        RuleFor(p => p).Custom((data, context) =>
        {
            var loanProduct = dbContext.LoanProducts.FirstOrDefault(x => x.Id == data.LoanProductId);
            if (loanProduct == null)
                context.AddFailure(
                  new ValidationFailure(nameof(data.LoanProductId),
                    "Loan product does not exist.", data.LoanProductId));
            
            
            var customer = dbContext.Customers.FirstOrDefault(x => x.Id == data.CustomerId);
            if (customer == null)
                context.AddFailure(
                    new ValidationFailure(nameof(data.CustomerId),
                        "Customer profile does not exist.", data.CustomerId));
            
            if (!loanProduct.MemberTypeIdList.Contains(customer.MemberType.ToString()))
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.LoanProductId),
                        "Customer member type not allowed to apply for this loan.", data.CustomerId));
            }

            if (!data.UseSpecialDeposit)
            {
                var isExist = dbContext.CustomerBankAccounts.Any(x => data.DestinationAccountId != null &&
                              x.CustomerId == data.CustomerId && x.Id == data.DestinationAccountId);
                if (!isExist)
                    context.AddFailure(
                      new ValidationFailure(nameof(data.DestinationAccountId),
                        "Existing bank account does not exist.", data.DestinationAccountId));
            }
            else
            {
                var isExist = dbContext.SpecialDepositAccounts.Any(x => data.SpecialDepositId != null &&
                                                                      x.Id == data.SpecialDepositId);
                if (!isExist)
                    context.AddFailure(
                      new ValidationFailure(nameof(data.DestinationAccountId),
                        "Special deposit account does not exist.", data.DestinationAccountId));
            }
            

            if (loanProduct.IsGuarantorRequired && (data.Guarantors == null || !data.Guarantors.Any()))
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Guarantors),
                        "Guarantor is required", data.Guarantors));
            }

            if (data.Guarantors != null && data.Guarantors.Any())
            {
                if (data.ApplicationUserId != null && data.Guarantors.Exists(x => x.GuarantorCustomerId == data.ApplicationUserId))
                {
                    context.AddFailure(
                            new ValidationFailure(nameof(data.Guarantors),
                                "You cannot select yourself as a guarantor", data.Guarantors));
                }

                var totalRetiree = data.Guarantors.Count(x => x.GuarantorType == GuarantorType.RETIREE);
                var totalRegular = data.Guarantors.Count(x => x.GuarantorType == GuarantorType.REGULAR);

                if (totalRetiree > loanProduct!.NonEmployeeGuarantorCount)
                {
                    context.AddFailure(
                            new ValidationFailure(nameof(data.Guarantors),
                                "Total retiree guarantor is more than the configured guarantor(s) in loan product.", data.Guarantors));
                }
                if (totalRegular > loanProduct!.EmployeeGuarantorCount)
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