using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public partial class CreateSpecialDepositAccountCommandValidator : AbstractValidator<CreateSpecialDepositAccountCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateSpecialDepositAccountCommandValidator> logger;
        public CreateSpecialDepositAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSpecialDepositAccountCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
            RuleFor(p => p).Custom((data, context) =>
                    {
                        var applicationExists = dbContext.SpecialDepositAccountApplications.Where(r => r.Id == data.ApplicationId).Any();
                        if (!applicationExists)
                        {
                            context.AddFailure(new ValidationFailure(nameof(data.ApplicationId), "Invalid ApplicationId.", data.ApplicationId));
                        }
                    });

  }


    }
}



