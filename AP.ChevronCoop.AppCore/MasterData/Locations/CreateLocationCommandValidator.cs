using AP.ChevronCoop.AppDomain.MasterData.Locations;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Locations;

public partial class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateLocationCommandValidator> logger;
    public CreateLocationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateLocationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.LocationType)
            .NotEmpty().WithMessage("Location type is required.")
            .MaximumLength(64).WithMessage("Location type cannot be longer than 64 characters.");

        RuleFor(x => x.ParentId)
            .MaximumLength(40).WithMessage("Parent ID cannot be longer than 40 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(64).WithMessage("Code cannot be longer than 64 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name cannot be longer than 128 characters.");

        RuleFor(x => x.SystemFlag)
            .NotNull().WithMessage("System flag is required.");

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

                    var checkName = dbContext.Locations.Where(r => r.Name.ToLower() == data.Name.ToLower()
            && r.CodeTypeId == data.CodeTypeId).Any();
                    if (checkName)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.Name),
                        "Duplicate names are not allowed.", data.Name));
                    }

                    var checkCode = dbContext.Locations.Where(r => r.Code.ToLower() == data.Code.ToLower()
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
