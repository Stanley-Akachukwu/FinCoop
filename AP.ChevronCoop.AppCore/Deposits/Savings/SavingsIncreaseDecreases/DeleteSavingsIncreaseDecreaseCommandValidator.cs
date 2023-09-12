
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsIncreaseDecreases;
public partial class DeleteSavingsIncreaseDecreaseCommandValidator : AbstractValidator<DeleteSavingsIncreaseDecreaseCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteSavingsIncreaseDecreaseCommandValidator> logger;
    public DeleteSavingsIncreaseDecreaseCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteSavingsIncreaseDecreaseCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.SavingsIncreaseDecreases.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            /*
            var checkChild = dbContext.ChildTable.Where(r => r.ChildTableId == data.Id).Any();
            if (checkChild)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected record has dependent records and cannot be deleted", data.Id));

            }
            */

        });

    }


}



