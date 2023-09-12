using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProductCharges
{
    public partial class UpdateDepositProductChargeCommandValidator : AbstractValidator<UpdateDepositProductChargeCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateDepositProductChargeCommandValidator> logger;
        public UpdateDepositProductChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateDepositProductChargeCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.ProductId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.ChargeId).NotEmpty().MaximumLength(80);

            RuleFor(p => p).Custom((data, context) =>
            {
                var checkId = dbContext.DepositProductCharges.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }
                var checkChargeId = dbContext.Charges.Where(r => r.Id == data.ChargeId).Any();
                if (!checkChargeId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ChargeId),
                    "Selected Charge Id does not exist", data.ChargeId));
                }
                var checkProductId = dbContext.DepositProducts.Where(r => r.Id == data.ProductId).Any();
                if (!checkProductId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ProductId),
                    "Selected Product Id does not exist", data.ProductId));
                }
            });
             
        }


    }

}


