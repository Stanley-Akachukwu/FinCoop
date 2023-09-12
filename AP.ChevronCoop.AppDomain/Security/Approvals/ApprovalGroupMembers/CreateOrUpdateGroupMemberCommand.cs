using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;

public class CreateOrUpdateGroupMemberCommand : IRequest<CommandResult<ApprovalGroupViewModel>>
{
	public string Id { get; set; }
	public string ApprovalGroupId { get; set; }
	public string MembershipId { get; set; }
	public string CreatedByUserId { get; set; }
}