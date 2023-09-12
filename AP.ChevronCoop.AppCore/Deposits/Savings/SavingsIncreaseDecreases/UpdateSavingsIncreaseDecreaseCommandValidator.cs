using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsIncreaseDecreases;

public partial class UpdateSavingsIncreaseDecreaseCommandValidator : AbstractValidator<UpdateSavingsIncreaseDecreaseCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateSavingsIncreaseDecreaseCommandValidator> logger;
    public UpdateSavingsIncreaseDecreaseCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSavingsIncreaseDecreaseCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;



        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.SavingsAccountId).NotNull().WithMessage("Account Id is Required");
        RuleFor(p => p.Amount).NotNull().WithMessage("Amount is Required");
        RuleFor(p => p.ContributionChangeRequest).IsInEnum();



        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.SavingsIncreaseDecreases.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

          
        });

    }


}



