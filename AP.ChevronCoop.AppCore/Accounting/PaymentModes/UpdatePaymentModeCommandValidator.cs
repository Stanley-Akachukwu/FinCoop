using AP.ChevronCoop.AppDomain.Accounting.PaymentModes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.PaymentModes;

public partial class UpdatePaymentModeCommandValidator : AbstractValidator<UpdatePaymentModeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdatePaymentModeCommandValidator> logger;
    public UpdatePaymentModeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdatePaymentModeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must be at most 128 characters.");

        RuleFor(p => p.Channel)
            .NotEmpty().WithMessage("Channel is required.")
            .MaximumLength(64).WithMessage("Channel must be at most 64 characters.");


        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.PaymentModes.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check Name Exists
            var checkName = dbContext.PaymentModes.Any(r => r.Name == data.Name && r.Id != data.Id);
            if (checkName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Payment mode: {data.Name} exist", data.Name));
            }

        });

    }


}
