using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositAccountApplications;
public partial class DeleteFixedDepositAccountApplicationCommandValidator : AbstractValidator<DeleteFixedDepositAccountApplicationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteFixedDepositAccountApplicationCommandValidator> logger;
    public DeleteFixedDepositAccountApplicationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteFixedDepositAccountApplicationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

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



