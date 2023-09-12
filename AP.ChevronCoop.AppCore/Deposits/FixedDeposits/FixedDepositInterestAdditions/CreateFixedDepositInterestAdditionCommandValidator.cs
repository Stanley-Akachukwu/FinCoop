using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestAdditions;
public partial class CreateFixedDepositInterestAdditionCommandValidator : AbstractValidator<CreateFixedDepositInterestAdditionCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateFixedDepositInterestAdditionCommandValidator> logger;
    public CreateFixedDepositInterestAdditionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateFixedDepositInterestAdditionCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;



        RuleFor(p => p.InterestEarned).NotNull();

        RuleFor(p => p.IsProcessed).NotNull();
        RuleFor(p => p.ProcessedDate).NotNull();






        RuleFor(p => p).Custom((data, context) =>
        {


            /*
            var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
            if (!parentExists)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.ParentId),
                "Invalid key.", data.ParentId));

            }

            var checkName = dbContext.FixedDepositInterestAdditions.Where(r => r.Name.ToLower() == data.Name.ToLower() 
            && r.CodeTypeId == data.CodeTypeId).Any();
            if (checkName)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Name),
                "Duplicate names are not allowed.", data.Name));
            }

            var checkCode = dbContext.FixedDepositInterestAdditions.Where(r => r.Code.ToLower() == data.Code.ToLower() 
            && r.CodeTypeId != data.CodeTypeId).Any();
            if (checkCode)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Code),
                "Duplicate codes are not allowed.", data.Code));
            }
            */

        });

    }


}



