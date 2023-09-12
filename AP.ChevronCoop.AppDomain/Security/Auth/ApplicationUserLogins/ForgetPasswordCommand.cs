using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins
{
    public class ForgetPasswordCommand : IRequest<CommandResult<ForgetPasswordViewModel>>
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
