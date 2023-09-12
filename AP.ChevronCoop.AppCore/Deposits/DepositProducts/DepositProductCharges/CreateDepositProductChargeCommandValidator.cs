using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProductCharges
{
    public partial class CreateDepositProductChargeCommandValidator : AbstractValidator<CreateDepositProductChargeCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateDepositProductChargeCommandValidator> logger;
        public CreateDepositProductChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateDepositProductChargeCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.ProductId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.ChargeId).NotEmpty().MaximumLength(80);

            RuleFor(p => p).Custom((data, context) =>
            {
                var checkChargeId = dbContext.Charges.Where(r => r.Id == data.ChargeId).Any();
                if (!checkChargeId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ChargeId),
                    "Selected Charge Id does not exist", data.ChargeId));
                }
                /*
                var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                if (!parentExists)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ParentId),
                    "Invalid key.", data.ParentId));

                }

                var checkName = dbContext.DepositProductCharges.Where(r => r.Name.ToLower() == data.Name.ToLower() 
				&& r.CodeTypeId == data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.DepositProductCharges.Where(r => r.Code.ToLower() == data.Code.ToLower() 
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


