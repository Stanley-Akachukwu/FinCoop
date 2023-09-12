using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;

public class CreateApprovalGroupMemberCommand: IRequest<CommandResult<ApprovalGroupMemberViewModel>>
{
    public string ApprovalGroupId { get; set; }
    public string MemberProfileId  { get; set; }
}