using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{
    public partial class ProfileValidator : AbstractValidator<MemberProfileViewModelResult>
    {
        public ProfileValidator()
        {
            RuleFor(p => p.membershipId).NotEmpty().WithMessage("Membership No. is required");
            RuleFor(p => p.firstName).NotEmpty().WithMessage("First name is required");
            RuleFor(p => p.lastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(p => p.gender).NotEmpty().WithMessage("Gender is required");
            RuleFor(p => p.state).NotEmpty().WithMessage("State is required");
            RuleFor(p => p.primaryEmail).NotEmpty().WithMessage("Primary email is required");
            RuleFor(p => p.primaryPhone).NotEmpty().WithMessage("Primary Phone Number is required");
            RuleFor(p => p.primaryEmail).Matches("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$").WithMessage("Email is not valid");
            RuleFor(p => p.secondaryEmail).Matches("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$").WithMessage("Email is not valid");
            RuleFor(p => p.primaryPhone).Matches("^0[0-9]{10}$").WithMessage("Phone number is not valid");
            RuleFor(p => p.secondaryPhone).Matches("^0[0-9]{10}$").WithMessage("Phone number is not valid");






        }
    }
}
