
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositAccountApplications;
public partial class UpdateFixedDepositAccountApplicationCommandValidator : AbstractValidator<UpdateFixedDepositAccountApplicationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateFixedDepositAccountApplicationCommandValidator> logger;
    public UpdateFixedDepositAccountApplicationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateFixedDepositAccountApplicationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.ApplicationNo).NotEmpty().MaximumLength(200);
        RuleFor(p => p.CustomerId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.DepositProductId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.Amount).NotNull();
        RuleFor(p => p.TenureUnit).NotEmpty().MaximumLength(128);
        RuleFor(p => p.TenureValue).NotNull();
        RuleFor(p => p.InterestRate).NotNull();
        RuleFor(p => p.MaturityInstructionType).NotEmpty().MaximumLength(-1);
        RuleFor(p => p.LiquidationAccountType).NotEmpty().MaximumLength(-1);



        RuleFor(p => p.ModeOfPayment).NotEmpty().MaximumLength(-1);



        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.FixedDepositAccountApplications.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            
            

        });

    }


}



