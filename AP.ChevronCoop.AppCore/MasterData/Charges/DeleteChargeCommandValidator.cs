using AP.ChevronCoop.AppDomain.MasterData.Charges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Charges;

public partial class DeleteChargeCommandValidator : AbstractValidator<DeleteChargeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteChargeCommandValidator> logger;
    public DeleteChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteChargeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.Charges.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                $"Selected Id does not exist", data.Id));
            }

        });

    }


}
