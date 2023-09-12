using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestAdditions
{
    public partial class CreateSpecialDepositInterestAdditionCommandValidator : AbstractValidator<CreateSpecialDepositInterestAdditionCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateSpecialDepositInterestAdditionCommandValidator> logger;
        public CreateSpecialDepositInterestAdditionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSpecialDepositInterestAdditionCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
            RuleFor(p => p.InterestEarned).NotNull(); 
 
            //RuleFor(p => p.IsProcessed).NotNull(); 
            //RuleFor(p => p.ProcessedDate).NotNull(); 

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

                var checkName = dbContext.SpecialDepositInterestAdditions.Where(r => r.Name.ToLower() == data.Name.ToLower() 
				&& r.CodeTypeId == data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositInterestAdditions.Where(r => r.Code.ToLower() == data.Code.ToLower() 
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



