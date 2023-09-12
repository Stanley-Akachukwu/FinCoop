using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;

public partial class UpdateSpecialDepositIncreaseDecreaseCommandValidator : AbstractValidator<UpdateSpecialDepositIncreaseDecreaseCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateSpecialDepositIncreaseDecreaseCommandValidator> logger;
    public UpdateSpecialDepositIncreaseDecreaseCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSpecialDepositIncreaseDecreaseCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;



        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.SpecialDepositAccountId).NotNull().WithMessage("Account Id is Required");
        RuleFor(p => p.Amount).NotNull().WithMessage("Amount is Required");
        RuleFor(p => p.ContributionChangeRequest).IsInEnum();



        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.SpecialDepositIncreaseDecreases.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }


        });

    }


}



