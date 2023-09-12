using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class KYCBioDataValidator : AbstractValidator<BioDataViewModel>
    {
        public KYCBioDataValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(p => p.PrimaryPhone).NotEmpty().WithMessage("Phone number is required");
            RuleFor(p => p.Gender).Must(gender => gender != "Select gender").WithMessage("Select your gender");
            //RuleFor(p => p.SecondaryEmail).NotEmpty().WithMessage("Secondary Email is required");
            // RuleFor(p => p.SecondaryEmail).Matches("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$").WithMessage("Secondary Email is not valid");
            /*.Matches(new Regex(@"[\d]")).WithMessage("PhoneNumber not valid")*/
            ;
            // RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required");
            //RuleFor(p => p.PrimaryPhone).Matches("^0[0-9]{9}$").WithMessage("Phone number is not valid");
            RuleFor(p => p.PrimaryPhone).Matches("^[1-9][0-9]{9}$").WithMessage("Phone number is not valid");
            //RuleFor(p => p.MembershipNumber).NotEmpty().WithMessage("Membership No. is required");

            //RuleFor(p => p.SecondaryPhone).Matches("^[1-9][0-9]{9}$").WithMessage("Secondary Phone number is not valid");

            RuleFor(p => p.SecondaryPhone)
            .Matches("^[1-9][0-9]{9}$")
            .Unless(p => string.IsNullOrEmpty(p.SecondaryPhone))
            .WithMessage("Phone number is not valid");


            RuleFor(p => p.SecondaryEmail)
            .Matches("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")
            .Unless(p => string.IsNullOrEmpty(p.SecondaryEmail))
            .WithMessage("Email is not valid");

            RuleFor(p => p.SecondaryPhone).Must((model, secondaryPhone) => secondaryPhone != model.PrimaryPhone)
            .WithMessage("Primary phone and secondary phone cannot be the same");



        }
    }
}

