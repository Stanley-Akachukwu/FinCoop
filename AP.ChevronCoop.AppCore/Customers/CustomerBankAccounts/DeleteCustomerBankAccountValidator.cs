using AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Customers.CustomerBankAccounts

{
    public class DeleteCustomerBankAccountValidator : AbstractValidator<DeleteCustomerBankAccountCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteCustomerBankAccountValidator> logger;
        public DeleteCustomerBankAccountValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteCustomerBankAccountValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(p => p).Custom((data, context) =>
            {
                var checkId = dbContext.CustomerBankAccounts.Any(r => r.Id == data.Id);
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }
            });
        }
    }
}