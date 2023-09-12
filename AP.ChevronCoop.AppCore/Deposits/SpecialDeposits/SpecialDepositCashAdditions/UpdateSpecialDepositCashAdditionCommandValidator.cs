using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public partial class UpdateSpecialDepositCashAdditionCommandValidator : AbstractValidator<UpdateSpecialDepositCashAdditionCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateSpecialDepositCashAdditionCommandValidator> logger;
        public UpdateSpecialDepositCashAdditionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSpecialDepositCashAdditionCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
		
			
		   RuleFor(p => p.Id).NotEmpty();

            RuleFor(x => x.Amount).NotEmpty().Must(x => x > 1000).WithMessage("Amount is required and must be greater than  N1,000.00.");

            RuleFor(x => x.ModeOfPayment).IsInEnum().WithMessage("Invalid Mode of Payment");
            RuleFor(p => p.IsProcessed).NotNull(); 

         RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.SpecialDepositCashAdditions.Where(r => r.Id == data.Id).Any();
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

                var checkName = dbContext.SpecialDepositCashAdditions.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositCashAdditions.Where(r => r.Id != data.Id &&
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


