using AP.ChevronCoop.AppDomain.Documents.DocumentTypes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.DocumentTypes;

public partial class CreateDocumentTypeCommandValidator : AbstractValidator<CreateDocumentTypeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateDocumentTypeCommandValidator> logger;
    public CreateDocumentTypeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateDocumentTypeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(256).WithMessage("Name must not exceed 256 characters.");

        RuleFor(x => x.SystemFlag)
            .NotNull().WithMessage("System flag is required.");

        RuleFor(p => p).Custom((data, context) =>
        {


        });

    }


}

