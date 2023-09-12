using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using FluentValidation;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{
    public partial class ValidateForgetPasswordOTPCommandValidator : AbstractValidator<ValidateForgetPasswordOTPCommand>
    {
        public ValidateForgetPasswordOTPCommandValidator()
        {
            RuleFor(p => p.OneTimePassword).NotEmpty().MaximumLength(8);
            RuleFor(p => p.OneTimePasswordCopy).NotEmpty().MaximumLength(8);
            RuleFor(p => p.Email).NotEmpty().MaximumLength(254);
            RuleFor(p => p).Custom((data, context) =>
            {
            });
        }
    }
}
