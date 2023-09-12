using AP.ChevronCoop.AppDomain.Documents.DocumentTypes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.DocumentTypes;

public partial class UpdateDocumentTypeCommandValidator : AbstractValidator<UpdateDocumentTypeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateDocumentTypeCommandValidator> logger;
    public UpdateDocumentTypeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateDocumentTypeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(256).WithMessage("Name must not exceed 256 characters.");

        RuleFor(x => x.SystemFlag)
            .NotNull().WithMessage("System flag is required.");


        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.DocumentTypes.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
