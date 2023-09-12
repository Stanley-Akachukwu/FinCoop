using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;

public class SwitchAccountCommand: IRequest<CommandResult<LoginViewModel>>
{
  public string UserId { get; set; }
  public bool SwitchToCorporate { get; set; }
}