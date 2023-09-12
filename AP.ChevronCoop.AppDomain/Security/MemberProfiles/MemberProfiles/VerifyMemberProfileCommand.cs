using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;

public class VerifyMemberProfileCommand: IRequest<CommandResult<string>>
{
  public string Token { get; set; }
  public string Email { get; set; }
}