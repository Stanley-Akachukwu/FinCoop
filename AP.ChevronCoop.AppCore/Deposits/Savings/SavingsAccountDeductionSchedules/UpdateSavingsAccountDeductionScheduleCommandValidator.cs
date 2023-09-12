using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccountDeductionSchedules;
public partial class UpdateSavingsAccountDeductionScheduleCommandValidator : AbstractValidator<UpdateSavingsAccountDeductionScheduleCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateSavingsAccountDeductionScheduleCommandValidator> logger;
    public UpdateSavingsAccountDeductionScheduleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSavingsAccountDeductionScheduleCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.SavingsAccountId).NotEmpty().MaximumLength(80);





        RuleFor(p => p.Amount).NotNull();


        RuleFor(p => p.DueDate).NotNull();







        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.SavingsAccountDeductionSchedules.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            /*
            var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
            if (!parentExists)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.ParentId),
                "Invalid key.", data.ParentId));

            }

            var checkName = dbContext.SavingsAccountDeductionSchedules.Where(r => r.Id != data.Id &&
            r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
            if (checkName)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Name),
                "Duplicate names are not allowed.", data.Name));
            }

            var checkCode = dbContext.SavingsAccountDeductionSchedules.Where(r => r.Id != data.Id &&
            r.Code.ToLower() == data.Code.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
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



