using AP.ChevronCoop.AppDomain.Documents.OfficePhotos;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.OfficePhotos;

public partial class DeleteOfficePhotoCommandValidator : AbstractValidator<DeleteOfficePhotoCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteOfficePhotoCommandValidator> logger;
    public DeleteOfficePhotoCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteOfficePhotoCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.OfficePhotos.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
