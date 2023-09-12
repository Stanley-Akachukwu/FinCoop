using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{
    public partial class UpdateSpecialDepositInterestScheduleCommandValidator : AbstractValidator<UpdateSpecialDepositInterestScheduleCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateSpecialDepositInterestScheduleCommandValidator> logger;
        public UpdateSpecialDepositInterestScheduleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSpecialDepositInterestScheduleCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
		
			
		 RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.ScheduleName).NotEmpty().MaximumLength(-1); 
        RuleFor(p => p.StartDate).NotNull(); 
        RuleFor(p => p.EndDate).NotNull(); 
        RuleFor(p => p.IsProcessed).NotNull(); 

        RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.SpecialDepositInterestSchedules.Where(r => r.Id == data.Id).Any();
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

                var checkName = dbContext.SpecialDepositInterestSchedules.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositInterestSchedules.Where(r => r.Id != data.Id &&
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


