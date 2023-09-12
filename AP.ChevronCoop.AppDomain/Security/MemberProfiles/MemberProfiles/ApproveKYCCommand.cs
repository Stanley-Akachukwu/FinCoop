using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;

public class ApproveKYCCommand: IRequest<CommandResult<MemberProfileViewModel>>
{
  public string ApprovedBy { get; set; }
  public string Status { get; set; }
  public string ApprovalId { get; set; }
  public string Comment { get; set; }
  public string MemberProfileId { get; set; }
}