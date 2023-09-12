using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestSchedules;
public partial class CreateFixedDepositInterestScheduleCommandValidator : AbstractValidator<CreateFixedDepositInterestScheduleCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateFixedDepositInterestScheduleCommandValidator> logger;
    public CreateFixedDepositInterestScheduleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateFixedDepositInterestScheduleCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.ScheduleName).NotEmpty().MaximumLength(-1);
        RuleFor(p => p.StartDate).NotNull();
        RuleFor(p => p.EndDate).NotNull();
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

            var checkName = dbContext.FixedDepositInterestSchedules.Where(r => r.Name.ToLower() == data.Name.ToLower() 
            && r.CodeTypeId == data.CodeTypeId).Any();
            if (checkName)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Name),
                "Duplicate names are not allowed.", data.Name));
            }

            var checkCode = dbContext.FixedDepositInterestSchedules.Where(r => r.Code.ToLower() == data.Code.ToLower() 
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



