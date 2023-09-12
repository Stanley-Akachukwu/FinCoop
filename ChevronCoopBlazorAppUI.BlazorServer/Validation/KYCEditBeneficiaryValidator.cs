using ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class KYCEditBeneficiaryValidator : AbstractValidator<BeneficiaryData>
    {
        public KYCEditBeneficiaryValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
                /*.Matches(new Regex(@"[\d]")).WithMessage("PhoneNumber not valid")*/;
            RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required");
           // RuleFor(p => p.PhoneNumber).Matches("^0[0-9]{10}$").WithMessage("Phone number is not valid");
            RuleFor(p => p.PhoneNumber).Matches("^[1-9][0-9]{9}$").WithMessage("Phone number is not valid");
            RuleFor(p => p.Email).Matches("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$").WithMessage("Email is not valid");




        }
    }
}
