using AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Customers.CustomerBankAccounts

{
    public class UpdateCustomerBankAccountValidator : AbstractValidator<UpdateCustomerBankAccountCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateCustomerBankAccountValidator> logger;
        public UpdateCustomerBankAccountValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateCustomerBankAccountValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty();
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

                var checkId = dbContext.CustomerBankAccounts.Any(r => r.Id == data.Id);
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }
                
                
                // Check checkDocumentTypeId Exists
                var checkBankId = dbContext.Banks.Any(r => r.Id == data.BankId);
                if (!checkBankId)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.BankId),
                            "Selected Bank Id does not exist", data.BankId));
                }
            });

        }


    }

}