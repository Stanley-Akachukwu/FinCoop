using AP.ChevronCoop.AppDomain.Accounting.PaymentModes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.PaymentModes;

public partial class CreatePaymentModeCommandValidator : AbstractValidator<CreatePaymentModeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreatePaymentModeCommandValidator> logger;
    public CreatePaymentModeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreatePaymentModeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must be at most 128 characters.");

        RuleFor(p => p.Channel)
            .NotEmpty().WithMessage("Channel is required.")
            .MaximumLength(64).WithMessage("Channel must be at most 64 characters.");

        RuleFor(p => p).Custom((data, context) =>
        {
            // Check Name Exists
            var checkName = dbContext.PaymentModes.Any(r => r.Name == data.Name);
            if (checkName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Payment mode: {data.Name} exist", data.Name));
            }
        });

    }


}
