using AP.ChevronCoop.AppDomain.Documents.OfficeSheets;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.OfficeSheets;

public partial class DeleteOfficeSheetCommandValidator : AbstractValidator<DeleteOfficeSheetCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteOfficeSheetCommandValidator> logger;
    public DeleteOfficeSheetCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteOfficeSheetCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.OfficeSheets.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
