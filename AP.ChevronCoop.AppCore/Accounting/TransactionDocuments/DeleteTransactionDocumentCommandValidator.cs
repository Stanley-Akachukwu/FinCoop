using AP.ChevronCoop.AppDomain.Accounting.TransactionDocuments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionDocuments;

public partial class DeleteTransactionDocumentCommandValidator : AbstractValidator<DeleteTransactionDocumentCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteTransactionDocumentCommandValidator> logger;
    public DeleteTransactionDocumentCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteTransactionDocumentCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.TransactionDocuments.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
