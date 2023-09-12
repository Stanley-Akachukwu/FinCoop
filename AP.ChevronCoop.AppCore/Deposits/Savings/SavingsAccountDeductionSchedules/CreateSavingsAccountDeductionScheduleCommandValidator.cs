using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccountDeductionSchedules;
public partial class CreateSavingsAccountDeductionScheduleCommandValidator : AbstractValidator<CreateSavingsAccountDeductionScheduleCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateSavingsAccountDeductionScheduleCommandValidator> logger;
    public CreateSavingsAccountDeductionScheduleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSavingsAccountDeductionScheduleCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.SavingsAccountId).NotEmpty().MaximumLength(80);





        RuleFor(p => p.Amount).NotNull();


        RuleFor(p => p.DueDate).NotNull();







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

            var checkName = dbContext.SavingsAccountDeductionSchedules.Where(r => r.Name.ToLower() == data.Name.ToLower() 
            && r.CodeTypeId == data.CodeTypeId).Any();
            if (checkName)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Name),
                "Duplicate names are not allowed.", data.Name));
            }

            var checkCode = dbContext.SavingsAccountDeductionSchedules.Where(r => r.Code.ToLower() == data.Code.ToLower() 
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




