using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules
{
    public partial class CreateSpecialDepositAccountDeductionScheduleCommandValidator : AbstractValidator<CreateSpecialDepositAccountDeductionScheduleCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateSpecialDepositAccountDeductionScheduleCommandValidator> logger;
        public CreateSpecialDepositAccountDeductionScheduleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSpecialDepositAccountDeductionScheduleCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
		
            RuleFor(p => p.SpecialDepositAccountId).NotEmpty().MaximumLength(80); 
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

                var checkName = dbContext.SpecialDepositAccountDeductionSchedules.Where(r => r.Name.ToLower() == data.Name.ToLower() 
				&& r.CodeTypeId == data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositAccountDeductionSchedules.Where(r => r.Code.ToLower() == data.Code.ToLower() 
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
	
    }


