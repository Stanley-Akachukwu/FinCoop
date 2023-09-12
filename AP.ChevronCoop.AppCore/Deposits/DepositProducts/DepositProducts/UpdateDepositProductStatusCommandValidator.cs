using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProducts
{
    public class UpdateDepositProductStatusCommandValidator : AbstractValidator<UpdateDepositProductStatusCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateDepositProductStatusCommandValidator> logger;

        public UpdateDepositProductStatusCommandValidator(ChevronCoopDbContext appDbContext,
          ILogger<UpdateDepositProductStatusCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.DepositProductId).NotEmpty();

            RuleFor(p => p.Status).IsInEnum();

            RuleFor(p => p).Custom((data, context) =>
            {
                var depositProductId = dbContext.DepositProducts.Any(r => r.Id == data.DepositProductId);
                if (!depositProductId)
                    context.AddFailure(
                      new ValidationFailure(nameof(data.DepositProductId),
                        "Selected Id does not exist", data.DepositProductId));
            });
        }
    }
}
