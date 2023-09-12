using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsCashAdditions;
public partial class UpdateSavingsCashAdditionCommandValidator : AbstractValidator<UpdateSavingsCashAdditionCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateSavingsCashAdditionCommandValidator> logger;
    public UpdateSavingsCashAdditionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSavingsCashAdditionCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.SavingsAccountId).NotNull().WithMessage("Account Id is Required");
        RuleFor(p => p.Amount).NotNull().WithMessage("Amount is Required");
        RuleFor(p => p.ModeOfPayment).IsInEnum();

       

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.SavingsCashAdditions.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}



