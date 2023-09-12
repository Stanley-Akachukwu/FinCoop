using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositLiquidations;
public partial class UpdateFixedDepositLiquidationCommandValidator : AbstractValidator<UpdateFixedDepositLiquidationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateFixedDepositLiquidationCommandValidator> logger;
    public UpdateFixedDepositLiquidationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateFixedDepositLiquidationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.FixedDepositAccountId).NotEmpty().WithMessage("Account is required");
        RuleFor(x => x.LiquidationAccountType).IsInEnum().WithMessage("Invalid liquidation account type.");




        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.FixedDepositLiquidations.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }
        });

    }


}



