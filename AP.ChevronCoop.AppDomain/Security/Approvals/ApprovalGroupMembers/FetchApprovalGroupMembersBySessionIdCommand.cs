using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;

public class FetchApprovalGroupMembersBySessionIdCommand : IRequest<CommandResult<List<ApprovalGroupMember>>>
{
	public string FetchByUserId { get; set; }
	public string ApprovalWorkflowSessionId { get; set; }
}