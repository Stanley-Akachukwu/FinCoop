using AP.ChevronCoop.AppDomain.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.GlobalCodes;

public partial class DeleteGlobalCodeCommandValidator : AbstractValidator<DeleteGlobalCodeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteGlobalCodeCommandValidator> logger;
    public DeleteGlobalCodeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteGlobalCodeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.GlobalCodes.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }
        });

    }


}
