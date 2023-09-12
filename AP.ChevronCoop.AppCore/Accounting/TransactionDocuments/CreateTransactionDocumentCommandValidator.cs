using AP.ChevronCoop.AppDomain.Accounting.TransactionDocuments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionDocuments;

public partial class CreateTransactionDocumentCommandValidator : AbstractValidator<CreateTransactionDocumentCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateTransactionDocumentCommandValidator> logger;
    public CreateTransactionDocumentCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateTransactionDocumentCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(d => d.DocumentNo).NotEmpty().WithMessage("DocumentNo is required.")
            .MaximumLength(64).WithMessage("DocumentNo must not exceed 64 characters.");

        RuleFor(d => d.TransactionJournalId).NotEmpty().WithMessage("TransactionJournalId is required.")
            .MaximumLength(40).WithMessage("TransactionJournalId must not exceed 40 characters.");

        RuleFor(d => d.DocumentTypeId).NotEmpty().WithMessage("DocumentTypeId is required.")
            .MaximumLength(40).WithMessage("DocumentTypeId must not exceed 40 characters.");

        RuleFor(d => d.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must not exceed 128 characters.");

        RuleFor(d => d.Document).NotNull().WithMessage("Document is required.");

        RuleFor(d => d.DocumentUrl).NotEmpty().WithMessage("DocumentUrl is required.");


        RuleFor(p => p).Custom((data, context) =>
        {
            // Check TransactionJournalId Exists
            var checkTransactionJournalId = dbContext.TransactionJournals.Any(r => r.Id == data.TransactionJournalId);
            if (!checkTransactionJournalId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.TransactionJournalId),
                        "Selected Transaction Journal Id does not exist", data.TransactionJournalId));
            }

            // Check DocumentTypeId Exists
            var checkDocumentTypeId = dbContext.DocumentTypes.Any(r => r.Id == data.DocumentTypeId);
            if (!checkDocumentTypeId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.DocumentTypeId),
                        "Selected Document Type Id does not exist", data.DocumentTypeId));
            }

            // Check DocumentNo Exists
            var checkDocumentNo = dbContext.TransactionDocuments.Any(r => r.DocumentNo == data.DocumentNo);
            if (checkDocumentNo)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.DocumentNo),
                        $"Document No: {data.DocumentNo} exist", data.DocumentNo));
            }
        });

    }


}
