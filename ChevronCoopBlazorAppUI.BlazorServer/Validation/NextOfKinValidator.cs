using ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember;
using FluentValidation;

namespace BlazorServer.Validation
{
    public class NextOfKinValidator : AbstractValidator<NextOfKinCreateModel>
    {
        public NextOfKinValidator()
        {
            RuleFor(p => p.firstName).NotEmpty().WithMessage("First name is required");
            RuleFor(p => p.lastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(p => p.phone).NotEmpty().WithMessage("Phone number is required");
            RuleFor(p => p.address).NotEmpty().WithMessage("Address is required");
            RuleFor(p => p.relationship).NotEmpty().WithMessage("Relationship is required");
          //  RuleFor(p => p.phone).Matches("^0[0-9]{10}$").WithMessage("Phone number is not valid");

            RuleFor(p => p.phone).Matches("^[1-9][0-9]{9}$").WithMessage("Phone number is not valid");

        }
    }
}
