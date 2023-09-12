using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProducts
{
    public partial class PublishDepositProductCommandValidator : AbstractValidator<PublishDepositProductCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<PublishDepositProductCommandValidator> logger;
        public PublishDepositProductCommandValidator(ChevronCoopDbContext appDbContext, ILogger<PublishDepositProductCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

             RuleFor(p => p.ProductId).NotEmpty().MaximumLength(64).WithMessage("Product for the publication not specified.");
            RuleFor(x => x.PublicationType).NotNull().WithMessage("Publication type not specified.");
            RuleFor(x => x.PublishedByUserId).NotNull().WithMessage("PublishedByUserId not specified.");
            RuleFor(p => p).Custom((data, context) =>
            {
                var checkProductExist = dbContext.DepositProducts.Where(r => r.Id == data.ProductId).Any();
                if (!checkProductExist)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ProductId),
                    "Invalid deposit product Id.", data.ProductId));
                }

                if (data.PublicationType == PublicationType.DEPARTMENT || data.PublicationType == PublicationType.CUSTOMER && data.Targets.Count < 1)
                    context.AddFailure(new ValidationFailure(nameof(data.Targets), "Publication targets not specified.", data.Targets));
            });
        }
    }
}


