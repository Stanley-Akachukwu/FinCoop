using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class BioDataValidator : AbstractValidator<MemberProfileMasterView>
    {
        public BioDataValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(p => p.PrimaryPhone).NotEmpty().WithMessage("Phone number is required")
                /*.Matches(new Regex(@"[\d]")).WithMessage("PhoneNumber not valid")*/;
            RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(p => p.PrimaryPhone).Matches("^0[0-9]{10}$").WithMessage("Phone number is not valid");
            RuleFor(p => p.MembershipId).NotEmpty().WithMessage("membership No. is required");
            RuleFor(p => p.SecondaryEmail).Equal(p => p.PrimaryEmail).WithMessage("Primary email and secondary email cannot be the same");
            //RuleFor(p => p.SecondaryEmail).NotEmpty().When(f => !string.IsNullOrEmpty(f.SecondaryEmail).WithMessage("Email is not valid");




        }
    }
}
