using AP.ChevronCoop.AppDomain.MasterData.Currencies;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;


namespace AP.ChevronCoop.AppCore.MasterData.Currencies
{
    public partial class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateCurrencyCommandValidator> logger;
        public UpdateCurrencyCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateCurrencyCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(16).WithMessage("Code must not exceed 16 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(128).WithMessage("Name must not exceed 128 characters.");

            RuleFor(x => x.Symbol)
                .NotEmpty().WithMessage("Symbol is required.")
                .MaximumLength(16).WithMessage("Symbol must not exceed 16 characters.");

            RuleFor(x => x.IsoSymbol)
                .MaximumLength(20).WithMessage("IsoSymbol must not exceed 20 characters.");

            RuleFor(x => x.DecimalPlaces)
                .GreaterThan(0).WithMessage("DecimalPlaces must be greater than 0.");

            RuleFor(x => x.Format)
                .MaximumLength(32).WithMessage("Format must not exceed 32 characters.");

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.Currencies.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }

                //NGN/23 NGN/56
                //var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                //if (!parentExists)
                //{
                //    context.AddFailure(
                //    new ValidationFailure(nameof(data.ParentId),
                //    "Invalid key.", data.ParentId));

                //}

                var checkName = dbContext.Currencies.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower()).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.Currencies.Where(r => r.Id != data.Id &&
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
