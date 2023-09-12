using AP.ChevronCoop.AppDomain.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.GlobalCodes;

public partial class CreateGlobalCodeCommandValidator : AbstractValidator<CreateGlobalCodeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateGlobalCodeCommandValidator> logger;
    public CreateGlobalCodeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateGlobalCodeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

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


            /*
                    var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                    if (!parentExists)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.ParentId),
                        "Invalid key.", data.ParentId));

                    }

                    var checkName = dbContext.GlobalCodes.Where(r => r.Name.ToLower() == data.Name.ToLower()
            && r.CodeTypeId == data.CodeTypeId).Any();
                    if (checkName)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.Name),
                        "Duplicate names are not allowed.", data.Name));
                    }

                    var checkCode = dbContext.GlobalCodes.Where(r => r.Code.ToLower() == data.Code.ToLower()
            && r.CodeTypeId != data.CodeTypeId).Any();
                    if (checkCode)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.Code),
                        "Duplicate codes are not allowed.", data.Code));
                    }
            */

        });

    }


}
