using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class PassWordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public PassWordValidator()
        {
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required");
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Password is required");
            RuleFor(p => p.NewPassword).Matches("[a-zA-Z0-9@#$%&*+\\-_(),+':;?.,![\\]\\s\\\\/]+$").WithMessage("Password is not valid");
            RuleFor(p => p.ConfirmPassword).Equal(p => p.NewPassword).WithMessage("Confirm password does not match");



        }
    }

}
