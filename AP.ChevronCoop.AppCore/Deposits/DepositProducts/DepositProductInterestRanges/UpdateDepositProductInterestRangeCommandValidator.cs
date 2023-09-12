using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProductInterestRanges
{
    public partial class UpdateDepositProductInterestRangeCommandValidator : AbstractValidator<UpdateDepositProductInterestRangeCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateDepositProductInterestRangeCommandValidator> logger;
        public UpdateDepositProductInterestRangeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateDepositProductInterestRangeCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.ProductId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.LowerLimit).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.UpperLimit).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.InterestRate).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.DepositProductInterestRanges.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }


                var checkProductId = dbContext.DepositProducts.Where(r => r.Id == data.ProductId).Any();
                if (!checkProductId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ProductId),
                    "Selected Product Id does not exist", data.ProductId));
                }

                if (data.LowerLimit >= data.UpperLimit)
                {
                    context.AddFailure(
                           new ValidationFailure(nameof(data.LowerLimit),
                           "Lower limit must be less than upper limit.", data.LowerLimit));
                }


            });

        }


    }

}


