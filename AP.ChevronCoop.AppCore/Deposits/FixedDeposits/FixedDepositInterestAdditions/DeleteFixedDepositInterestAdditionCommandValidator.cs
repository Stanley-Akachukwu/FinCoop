using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestAdditions;
public partial class DeleteFixedDepositInterestAdditionCommandValidator : AbstractValidator<DeleteFixedDepositInterestAdditionCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteFixedDepositInterestAdditionCommandValidator> logger;
    public DeleteFixedDepositInterestAdditionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteFixedDepositInterestAdditionCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.FixedDepositInterestAdditions.Where(r => r.Id == data.Id).Any();
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



