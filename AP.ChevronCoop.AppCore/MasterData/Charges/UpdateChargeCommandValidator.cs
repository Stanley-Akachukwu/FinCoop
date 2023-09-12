using AP.ChevronCoop.AppDomain.MasterData.Charges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Charges;

public partial class UpdateChargeCommandValidator : AbstractValidator<UpdateChargeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateChargeCommandValidator> logger;
    public UpdateChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateChargeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(32).WithMessage("Code cannot exceed 32 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name cannot exceed 128 characters.");


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

            var checkId = dbContext.Charges.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check Currency Id Exists
            var checkCurrencyId = dbContext.Currencies.Any(r => r.Id == data.CurrencyId);
            if (!checkCurrencyId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.CurrencyId),
                        "Selected currency Id does not exist", data.CurrencyId));
            }

            // Check Charge Name Exists
            var checkChargeName = dbContext.Charges.Any(r => r.Name == data.Name && r.Id != data.Id);
            if (checkChargeName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Charge name {data.Name} already exists", data.Name));
            }

            // Check Charge Code Exists
            var checkChargeCode = dbContext.Charges.Any(r => r.Code == data.Code && r.Id != data.Id);
            if (checkChargeCode)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                        $"Charge name {data.Code} already exists", data.Code));
            }

        });

    }


}
