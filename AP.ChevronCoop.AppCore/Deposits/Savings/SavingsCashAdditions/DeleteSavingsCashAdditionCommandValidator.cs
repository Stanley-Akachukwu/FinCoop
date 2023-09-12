using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsCashAdditions;
public partial class DeleteSavingsCashAdditionCommandValidator : AbstractValidator<DeleteSavingsCashAdditionCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteSavingsCashAdditionCommandValidator> logger;
    public DeleteSavingsCashAdditionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteSavingsCashAdditionCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.SavingsCashAdditions.Where(r => r.Id == data.Id).Any();
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



