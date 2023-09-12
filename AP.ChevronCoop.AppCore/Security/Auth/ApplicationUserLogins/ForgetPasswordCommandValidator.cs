using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using FluentValidation;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{

    public partial class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
    {
        public ForgetPasswordCommandValidator()
        {
            RuleFor(p => p.Email).NotEmpty().MaximumLength(254);
            RuleFor(p => p).Custom((data, context) =>
            {
            });
        }
    }
}
