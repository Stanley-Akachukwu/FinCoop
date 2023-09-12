using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

public class UpdateApprovalGroupCommand :  IRequest<CommandResult<ApprovalGroupViewModel>>
{
	public string Id { get; set; }
    public string Name { get; set; }
    public List<string> ApprovalGroupMemberIds { get; set; }
    public string UpdatedByUserId { get; set; }
}