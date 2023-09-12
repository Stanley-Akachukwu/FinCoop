using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberNextOfKins;

public class CreateMemberNextOfKinCommand: CreateCommand, IRequest<CommandResult<MemberNextOfKinViewModel>>
{
  public string ProfileId { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public string Relationship { get; set; }
  public string Address { get; set; }
}