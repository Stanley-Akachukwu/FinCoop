using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProducts
{
    public partial class CreateDepositProductCommandValidator : AbstractValidator<CreateDepositProductCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateDepositProductCommandValidator> logger;
        public CreateDepositProductCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateDepositProductCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

    

            RuleFor(p => p.Code).NotEmpty().MaximumLength(64);
            RuleFor(p => p.Name).NotEmpty().MaximumLength(256);
            RuleFor(p => p.ShortName).NotEmpty().MaximumLength(256);
            RuleFor(p => p.MinimumAge).NotNull();
            RuleFor(p => p.MaximumAge).NotNull();
           
            RuleFor(p => p.TenureValue).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.DefaultCurrencyId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.IsInterestEnabled).NotNull();
            RuleFor(p => p.ApprovalWorkflowId).NotNull();
            RuleFor(p => p.CreatedByUserId).NotNull().WithMessage("CreatedByUserId is required.");

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkCurrency = dbContext.Currencies.Where(r => r.Id == data.DefaultCurrencyId).Any();
                if (!checkCurrency)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.DefaultCurrencyId),
                    "Invalid currency Id.", data.DefaultCurrencyId));
                }

                //var hasInterest = data != null && data.InterestRanges.Any();
                //if (data.IsInterestEnabled && !hasInterest)
                //{
                //    context.AddFailure(
                //   new ValidationFailure(nameof(data.InterestRanges),
                //   "Interest ranges cannot be empty", data.InterestRanges));
                //}


                var checkName = dbContext.DepositProducts.Where(r => r.Name.ToLower() == data.Name.ToLower()).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkName2 = dbContext.DepositProducts.Where(r => r.ShortName.ToLower() == data.ShortName.ToLower()).Any();
                if (checkName2)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ShortName),
                    "Duplicate short names are not allowed.", data.ShortName));
                }

                var checkCode = dbContext.DepositProducts.Where(r => r.Code.ToLower() == data.Code.ToLower()).Any();
                if (checkCode)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                    "Duplicate codes are not allowed.", data.Code));
                }

                if (data.Tenure !=Tenure.NONE && data.TenureValue <= 0)
                    context.AddFailure(new ValidationFailure(nameof(data.Tenure), "Invalid Tenure value passed.", data.Tenure));

                if (data.ProductType <= 0)
                    context.AddFailure(new ValidationFailure(nameof(data.Tenure), "Invalid Deposit Product Type  passed.", data.Tenure));

            });
        }


    }

}


