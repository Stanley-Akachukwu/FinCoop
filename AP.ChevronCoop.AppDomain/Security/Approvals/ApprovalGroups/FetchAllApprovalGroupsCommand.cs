using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

public class FetchAllApprovalGroupsCommand : IRequest<CommandResult<List<ApprovalGroupViewModel>>>
{
	public string FetchByUserId { get; set; }
}