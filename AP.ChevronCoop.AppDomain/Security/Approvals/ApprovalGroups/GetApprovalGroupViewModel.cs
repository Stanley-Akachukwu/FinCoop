using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

public class GetApprovalGroupViewModel
{
	public string Id { get; set; }
	public string Name { get; set; }
	public List<ApprovalGroupMemberViewModel> Members { get; set; }
}