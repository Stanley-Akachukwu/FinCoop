using AP.ChevronCoop.AppDomain.Documents.OfficePhotos;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.OfficePhotos;

public partial class UpdateOfficePhotoCommandValidator : AbstractValidator<UpdateOfficePhotoCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateOfficePhotoCommandValidator> logger;
    public UpdateOfficePhotoCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateOfficePhotoCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(d => d.DocumentNo).NotEmpty().WithMessage("Document number is required.")
            .MaximumLength(64).WithMessage("Document number cannot exceed 64 characters.");

        RuleFor(d => d.DocumentTypeId).NotEmpty().WithMessage("Document type ID is required.")
            .MaximumLength(40).WithMessage("Document type ID cannot exceed 40 characters.");

        RuleFor(d => d.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(256).WithMessage("Name cannot exceed 256 characters.");

        RuleFor(d => d.Document).NotEmpty().WithMessage("Document is required.");

        RuleFor(d => d.MimeType).MaximumLength(64).WithMessage("MIME type cannot exceed 64 characters.");

        RuleFor(d => d.FilePath).MaximumLength(400).WithMessage("File path cannot exceed 400 characters.");


        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.OfficePhotos.Any(r => r.Id == data.Id);
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
