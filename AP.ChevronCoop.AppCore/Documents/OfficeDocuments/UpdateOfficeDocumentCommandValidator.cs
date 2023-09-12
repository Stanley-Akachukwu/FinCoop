using AP.ChevronCoop.AppDomain.Documents.OfficeDocuments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.OfficeDocuments;

public partial class UpdateOfficeDocumentCommandValidator : AbstractValidator<UpdateOfficeDocumentCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateOfficeDocumentCommandValidator> logger;
    public UpdateOfficeDocumentCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateOfficeDocumentCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(doc => doc.DocumentNo)
            .NotEmpty().WithMessage("Document Number is required.")
            .MaximumLength(64).WithMessage("Document Number cannot be longer than 64 characters.");

        RuleFor(doc => doc.DocumentTypeId)
            .NotEmpty().WithMessage("Document Type is required.")
            .MaximumLength(40).WithMessage("Document Type cannot be longer than 40 characters.");

        RuleFor(doc => doc.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(256).WithMessage("Name cannot be longer than 256 characters.");

        RuleFor(doc => doc.MimeType)
            .MaximumLength(64).WithMessage("MIME Type cannot be longer than 64 characters.");

        RuleFor(doc => doc.FilePath)
            .MaximumLength(400).WithMessage("File path cannot be longer than 400 characters.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.OfficeDocuments.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check checkDocumentTypeId Exists
            var checkDocumentTypeId = dbContext.DocumentTypes.Any(r => r.Id == data.DocumentTypeId);
            if (!checkDocumentTypeId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.DocumentTypeId),
                        "Selected Document Type Id does not exist", data.DocumentTypeId));
            }

        });

    }


}
