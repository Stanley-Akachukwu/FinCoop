using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
public partial class UpdateFixedDepositChangeInMaturityCommandValidator : AbstractValidator<UpdateFixedDepositChangeInMaturityCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateFixedDepositChangeInMaturityCommandValidator> logger;
    public UpdateFixedDepositChangeInMaturityCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateFixedDepositChangeInMaturityCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.FixedDepositAccountId).NotEmpty().WithMessage("Account is required");
        RuleFor(x => x.MaturityInstructionType).IsInEnum().WithMessage("Invalid Instruction type.");
        RuleFor(x => x.LiquidationAccountType).IsInEnum().WithMessage("Invalid liquidation account type.");





        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.FixedDepositChangeInMaturities.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}



