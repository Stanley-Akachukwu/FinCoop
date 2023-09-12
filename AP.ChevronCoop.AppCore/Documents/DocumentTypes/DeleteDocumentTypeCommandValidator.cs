using AP.ChevronCoop.AppDomain.Documents.DocumentTypes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.DocumentTypes;

public partial class DeleteDocumentTypeCommandValidator : AbstractValidator<DeleteDocumentTypeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteDocumentTypeCommandValidator> logger;
    public DeleteDocumentTypeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteDocumentTypeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

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
