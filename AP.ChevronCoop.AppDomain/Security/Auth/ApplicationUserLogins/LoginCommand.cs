using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;

public class LoginCommand: IRequest<CommandResult<LoginViewModel>>
{
  public string Email { get; set; }
  public string Password { get; set; }
}