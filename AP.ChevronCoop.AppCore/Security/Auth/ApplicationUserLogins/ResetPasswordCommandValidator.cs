using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using FluentValidation;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{

    public partial class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(p => p.NewPassword).NotEmpty().MaximumLength(256);
            RuleFor(p => p.ConfirmPassword).NotEmpty().MaximumLength(256);
            RuleFor(p => p.Email).NotEmpty().MaximumLength(254);
            RuleFor(p => p.OneTimePassword).NotEmpty();
            RuleFor(p => p).Custom((data, context) =>
            {
            });
        }
    }
}

