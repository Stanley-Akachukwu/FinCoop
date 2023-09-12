using AP.ChevronCoop.AppDomain.MasterData.Charges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Charges;

public partial class CreateChargeCommandValidator : AbstractValidator<CreateChargeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateChargeCommandValidator> logger;
    public CreateChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateChargeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(fee => fee.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(32).WithMessage("Code length cannot exceed 32 characters.");

        RuleFor(fee => fee.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name length cannot exceed 128 characters.");




        RuleFor(fee => fee.Method).IsEnumName(typeof(ChargeMethod))
            .NotEmpty().WithMessage("Method is required.")
            .MaximumLength(32).WithMessage("Method length cannot exceed 32 characters.");

        RuleFor(fee => fee.Target).IsEnumName(typeof(ChargeTarget))
            .NotEmpty().WithMessage("Target is required.")
            .MaximumLength(32).WithMessage("Target length cannot exceed 32 characters.");

        RuleFor(fee => fee.CalculationMethod).IsEnumName(typeof(ChargeCalculationMethod))
            .NotEmpty().WithMessage("CalculationMethod is required.")
            .MaximumLength(32).WithMessage("CalculationMethod length cannot exceed 32 characters.");

        RuleFor(fee => fee.CurrencyId)
            .NotEmpty().WithMessage("CurrencyId is required.")
            .MaximumLength(40).WithMessage("CurrencyId length cannot exceed 40 characters.");

        RuleFor(fee => fee.ChargeValue)
            .GreaterThanOrEqualTo(0).WithMessage("ChargeValue must be greater than or equal to 0.");

        //RuleFor(fee => fee.ChargeValue)
        //    .InclusiveBetween(0, 100).WithMessage("Percent must be between 0 and 100.");

        //RuleFor(fee => fee.MaximumCharge)
        //    .Must((fee, maximumFee) => maximumFee == null || maximumFee >= fee.FlatFee).WithMessage("MaximumFee must be greater than or equal to FlatFee.");

        //RuleFor(fee => fee.MinimimumCharge)
        //    .Must((fee, minimumFee) => minimumFee == null || minimumFee <= fee.FlatFee).WithMessage("MinimumFee must be less than or equal to FlatFee.");

        RuleFor(p => p).Custom((data, context) =>
        {
            // Check Currency Id Exists
            var checkCurrencyId = dbContext.Currencies.Any(r => r.Id == data.CurrencyId);
            if (!checkCurrencyId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.CurrencyId),
                        "Selected calendar Id does not exist", data.CurrencyId));
            }

            // Check Charge Name Exists
            var checkChargeName = dbContext.Charges.Any(r => r.Name == data.Name);
            if (checkChargeName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Charge name: {data.Name} already exists", data.Name));
            }

            // Check Charge Code Exists
            var checkChargeCode = dbContext.Charges.Any(r => r.Code == data.Code);
            if (checkChargeCode)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                        $"Charge code: {data.Code} already exists", data.Code));
            }
        });

    }


}
