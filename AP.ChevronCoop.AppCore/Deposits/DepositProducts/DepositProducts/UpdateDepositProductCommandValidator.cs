using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Backup.DepositProducts
{
    public partial class UpdateDepositProductCommandValidator : AbstractValidator<UpdateDepositProductCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateDepositProductCommandValidator> logger;
        public UpdateDepositProductCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateDepositProductCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Code).NotEmpty().MaximumLength(64);
            RuleFor(p => p.Name).NotEmpty().MaximumLength(256);
            RuleFor(p => p.ShortName).NotEmpty().MaximumLength(256);
            RuleFor(p => p.MinimumAge).NotNull();
            RuleFor(p => p.MaximumAge).NotNull();
            RuleFor(p => p.Tenure).NotNull();
            //RuleFor(fee => fee.Tenure).IsEnumName(typeof(Tenure))
            //.NotEmpty().WithMessage("Tenure is required.")
            //.MaximumLength(32).WithMessage("Tenure length cannot exceed 32 characters.");
            RuleFor(p => p.TenureValue).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.DefaultCurrencyId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.ApprovalWorkflowId).NotNull();
            RuleFor(p => p.IsInterestEnabled).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.DepositProducts.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }

                var checkCurrency = dbContext.Currencies.Where(r => r.Id == data.DefaultCurrencyId).Any();
                if (!checkCurrency)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.DefaultCurrencyId),
                    "Invalid currency Id.", data.DefaultCurrencyId));
                }

                var checkName = dbContext.DepositProducts.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower()).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }


                var checkName2 = dbContext.DepositProducts.Where(r => r.Id != data.Id &&
                r.ShortName.ToLower() == data.ShortName.ToLower()).Any();
                if (checkName2)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ShortName),
                    "Duplicate short names are not allowed.", data.ShortName));
                }

                var checkCode = dbContext.DepositProducts.Where(r => r.Id != data.Id &&
                r.Code.ToLower() == data.Code.ToLower()).Any();
                if (checkCode)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                    "Duplicate codes are not allowed.", data.Code));
                }
            });
        }
    }
}
