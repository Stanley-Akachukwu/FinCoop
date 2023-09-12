using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;

public class UpdateUserStatusCommand: IRequest<CommandResult<string>>
{
  public string UserId { get; set; }
  public string Status { get; set; }
}