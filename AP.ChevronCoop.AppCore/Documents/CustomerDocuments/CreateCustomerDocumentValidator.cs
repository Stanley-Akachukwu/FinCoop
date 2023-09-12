using AP.ChevronCoop.AppDomain.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace AP.ChevronCoop.AppCore.Documents.CustomerDocuments
{
    public class CreateCustomerDocumentValidator : AbstractValidator<CreateCustomerPaymentDocumentCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;

        public CreateCustomerDocumentValidator(ChevronCoopDbContext dbContext)
        {

            _dbContext = dbContext;

            RuleFor(p => p.CustomerId).NotEmpty();
            RuleFor(p => p.Document).NotEmpty();
            RuleFor(p => p.FileSize).NotEmpty();
            RuleFor(p => p.FileName).NotEmpty().MaximumLength(100);
            RuleFor(p => p.MimeType).NotEmpty().MaximumLength(10);
            RuleFor(p => p.DocumentType).IsInEnum();

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.Customers.Where(r => r.Id == data.CustomerId).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.CustomerId),
                    "Member does not exist", data.CustomerId));
                }

            });

        }
    }
}
