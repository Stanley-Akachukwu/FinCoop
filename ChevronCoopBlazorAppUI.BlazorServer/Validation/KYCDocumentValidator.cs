using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class KYCDocumentValidator : AbstractValidator<KYCDocumentViewModel>
    {
        public KYCDocumentValidator()
        {
            RuleFor(p => p.DocumentNumber).NotEmpty().WithMessage("Identification Number is required");
            RuleFor(p => p.DocumentTypeId).NotEmpty().WithMessage("Document Type is required");

        }
    }
}
