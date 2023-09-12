using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;

public class DeleteApprovalGroupMemberCommand : DeleteCommand, IRequest<CommandResult<string>>
{
	public string ApprovalGroupId { get; set; }
	public string Id { get; set; }
	public string Email { get; set; }
}