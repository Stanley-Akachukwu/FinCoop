using AP.ChevronCoop.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins
{
	public class ValidateForgetPasswordOTPCommand : IRequest<CommandResult<ValidateForgetPasswordOTPViewModel>>
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string OneTimePassword { get; set; }
		[Required]
		public string OneTimePasswordCopy { get; set; }
	}
}
