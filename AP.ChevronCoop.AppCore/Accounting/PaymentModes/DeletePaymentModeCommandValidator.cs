using AP.ChevronCoop.AppDomain.Accounting.PaymentModes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.PaymentModes;

public partial class DeletePaymentModeCommandValidator : AbstractValidator<DeletePaymentModeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeletePaymentModeCommandValidator> logger;
    public DeletePaymentModeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeletePaymentModeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.PaymentModes.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
