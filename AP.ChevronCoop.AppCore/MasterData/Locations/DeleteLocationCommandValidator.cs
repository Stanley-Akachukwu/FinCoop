using AP.ChevronCoop.AppDomain.MasterData.Locations;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Locations;

public partial class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteLocationCommandValidator> logger;
    public DeleteLocationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteLocationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.Locations.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }
        });

    }


}
