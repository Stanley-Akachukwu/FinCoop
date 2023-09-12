using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccountApplications;
public partial class UpdateSavingsAccountApplicationCommandValidator : AbstractValidator<UpdateSavingsAccountApplicationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateSavingsAccountApplicationCommandValidator> logger;
    public UpdateSavingsAccountApplicationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSavingsAccountApplicationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.ApplicationNo).NotEmpty().MaximumLength(200);
        RuleFor(p => p.CustomerId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.DepositProductId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.Amount).NotNull();

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.SavingsAccountApplications.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

           
        });

    }


}



