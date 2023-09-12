using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;
public partial class CreateFixedDepositInterestScheduleItemCommandValidator : AbstractValidator<CreateFixedDepositInterestScheduleItemCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateFixedDepositInterestScheduleItemCommandValidator> logger;
    public CreateFixedDepositInterestScheduleItemCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateFixedDepositInterestScheduleItemCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;



        RuleFor(p => p.OldBalance).NotNull();
        RuleFor(p => p.PeriodCashAddition).NotNull();
        RuleFor(p => p.InterestRate).NotNull();
        RuleFor(p => p.InterestEarned).NotNull();
        RuleFor(p => p.NewBalance).NotNull();







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

            var checkName = dbContext.FixedDepositInterestScheduleItems.Where(r => r.Name.ToLower() == data.Name.ToLower() 
            && r.CodeTypeId == data.CodeTypeId).Any();
            if (checkName)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Name),
                "Duplicate names are not allowed.", data.Name));
            }

            var checkCode = dbContext.FixedDepositInterestScheduleItems.Where(r => r.Code.ToLower() == data.Code.ToLower() 
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



