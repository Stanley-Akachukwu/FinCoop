using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProductInterestRanges
{
    public partial class CreateDepositProductInterestRangeCommandValidator : AbstractValidator<CreateDepositProductInterestRangeCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateDepositProductInterestRangeCommandValidator> logger;
        public CreateDepositProductInterestRangeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateDepositProductInterestRangeCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.ProductId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.LowerLimit).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.UpperLimit).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.InterestRate).NotNull().GreaterThanOrEqualTo(0);

            RuleFor(p => p).Custom((data, context) =>
            {

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


