using AP.ChevronCoop.AppDomain.Security.MemberProfiles.RetireeSwitches;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;

public class CreateRetireeSwitchCommand : IRequest<CommandResult<MemberProfileViewModel>>
{
  public string MemberProfileId { get; set; }
  public string InitiatedBy { get; set; }
  public string Description { get; set; }
}