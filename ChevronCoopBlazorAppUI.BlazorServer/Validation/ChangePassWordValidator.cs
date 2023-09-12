using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{


    public class ChangePassWordValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePassWordValidator()
        {
            RuleFor(p => p.OldPassword).NotEmpty().WithMessage("Current Password is required");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required");
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Password is required");
            RuleFor(p => p.NewPassword).Matches("(?=.*[A-Z])(?=.*[a-zA-Z0-9])(?=.*[@$!%*?&])[A-Za-z\\\\d@$!%*?&]{8,}").WithMessage("Password is not valid (Please comply with the instruction below)");
            RuleFor(p => p.ConfirmPassword).Equal(p => p.NewPassword).WithMessage("Confirm password does not match");



        }
    }
}
