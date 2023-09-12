using ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class NextOfKinDataValidation : AbstractValidator<NextOfKinData>
    {
        public NextOfKinDataValidation()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(p => p.Relationship).NotEmpty().WithMessage("Relationship is required");
           // RuleFor(p => p.PhoneNumber).Matches("^0[0-9]{10}$").WithMessage("Phone number is not valid");

            RuleFor(p => p.PhoneNumber).Matches("^[1-9][0-9]{9}$").WithMessage("Phone number is not valid");


        }
    }
}
