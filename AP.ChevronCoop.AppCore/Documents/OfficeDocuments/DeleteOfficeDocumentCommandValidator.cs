using AP.ChevronCoop.AppDomain.Documents.OfficeDocuments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.OfficeDocuments;

public partial class DeleteOfficeDocumentCommandValidator : AbstractValidator<DeleteOfficeDocumentCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteOfficeDocumentCommandValidator> logger;
    public DeleteOfficeDocumentCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteOfficeDocumentCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.OfficeDocuments.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
