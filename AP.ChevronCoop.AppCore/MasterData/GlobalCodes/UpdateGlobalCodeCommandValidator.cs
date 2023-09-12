using AP.ChevronCoop.AppDomain.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.GlobalCodes;

public partial class UpdateGlobalCodeCommandValidator : AbstractValidator<UpdateGlobalCodeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateGlobalCodeCommandValidator> logger;
    public UpdateGlobalCodeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateGlobalCodeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.CodeType)
            .NotEmpty().WithMessage("Code Type is required.")
            .MaximumLength(64).WithMessage("Code Type must not exceed 64 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(128).WithMessage("Code must not exceed 128 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(256).WithMessage("Name must not exceed 256 characters.");

        RuleFor(x => x.SystemFlag)
            .NotNull().WithMessage("System Flag is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.GlobalCodes.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }



        });

    }


}
