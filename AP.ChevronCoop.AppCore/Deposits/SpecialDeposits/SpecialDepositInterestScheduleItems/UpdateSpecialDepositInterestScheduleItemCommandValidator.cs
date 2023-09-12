using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public partial class UpdateSpecialDepositInterestScheduleItemCommandValidator : AbstractValidator<UpdateSpecialDepositInterestScheduleItemCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateSpecialDepositInterestScheduleItemCommandValidator> logger;
        public UpdateSpecialDepositInterestScheduleItemCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSpecialDepositInterestScheduleItemCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
		
			
		 RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.OldBalance).NotNull(); 
        RuleFor(p => p.PeriodCashAddition).NotNull(); 
        RuleFor(p => p.InterestRate).NotNull(); 
        RuleFor(p => p.InterestEarned).NotNull(); 
        RuleFor(p => p.NewBalance).NotNull(); 

        RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.SpecialDepositInterestScheduleItems.Where(r => r.Id == data.Id).Any();
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

                var checkName = dbContext.SpecialDepositInterestScheduleItems.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositInterestScheduleItems.Where(r => r.Id != data.Id &&
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
	
 
    }

