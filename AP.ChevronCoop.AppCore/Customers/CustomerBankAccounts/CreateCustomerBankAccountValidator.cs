using AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Customers.CustomerBankAccounts
{
    public class CreateCustomerBankAccountValidator : AbstractValidator<CreateCustomerBankAccountCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateCustomerBankAccountValidator> logger;
        public CreateCustomerBankAccountValidator(ChevronCoopDbContext appDbContext, ILogger<CreateCustomerBankAccountValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.")
                .MaximumLength(40).WithMessage("Customer ID must not exceed 40 characters.");

            RuleFor(x => x.BankId)
                .NotEmpty().WithMessage("Bank ID is required.")
                .MaximumLength(40).WithMessage("Bank ID must not exceed 40 characters.");

            // RuleFor(x => x.BVN)
            //     .NotEmpty().WithMessage("BVN is required.")
            //     .MaximumLength(64).WithMessage("BVN must not exceed 64 characters.");

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Account number is required.")
                .MaximumLength(32).WithMessage("Account number must not exceed 32 characters.");

            RuleFor(x => x.AccountName)
                .NotEmpty().WithMessage("Account name is required.")
                .MaximumLength(128).WithMessage("Account name must not exceed 128 characters.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var checkCustomerId = dbContext.Customers.Any(r => r.Id == data.CustomerId);
                if (!checkCustomerId)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.CustomerId),
                            "Customer Id does not exist", data.CustomerId));
                }
                
                // Check checkDocumentTypeId Exists
                var checkBankId = dbContext.Banks.Any(r => r.Id == data.BankId);
                if (!checkBankId)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.BankId),
                            "Selected Bank Id does not exist", data.BankId));
                }
                
                
                if (!string.IsNullOrWhiteSpace(data.AccountName))
                {

                    // var checkAccountName = dbContext.CustomerBankAccounts.Any(r => r.AccountName.ToLower() == data.AccountName.ToLower()
                    //     && r.Id != data.Id);
                    // if (checkAccountName)
                    // {
                    //     context.AddFailure(
                    //         new ValidationFailure(nameof(data.AccountName),
                    //             "Duplicate accounts are not allowed.", data.AccountName));
                    // }

                    // var checkBVN = dbContext.CustomerBankAccounts.Any(r => r.BVN == data.BVN && r.Id != data.Id);
                    // if (checkBVN)
                    // {
                    //     context.AddFailure(
                    //         new ValidationFailure(nameof(data.BVN),
                    //             "Duplicate account are not allowed.", data.BVN));
                    // }

                    
                }
            });
        }
    }
}