using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{
    public class BeneficiaryValidator : AbstractValidator<MyBeneficiaryCreateModel>
    {
        public BeneficiaryValidator()
        {
            RuleFor(p => p.firstName).NotEmpty().WithMessage("First name is required");
            RuleFor(p => p.lastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(p => p.phone).NotEmpty().WithMessage("Phone number is required")
                /*.Matches(new Regex(@"[\d]")).WithMessage("PhoneNumber not valid")*/;
            RuleFor(p => p.address).NotEmpty().WithMessage("Address is required");
            //RuleFor(p => p.phone).Matches("^0[0-9]{10}$").WithMessage("Phone number is not valid");
            RuleFor(p => p.phone).Matches("^[1-9][0-9]{9}$").WithMessage("Phone number is not valid");
            RuleFor(p => p.email).Matches("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$").WithMessage("Email is not valid");



        }
    }

}
