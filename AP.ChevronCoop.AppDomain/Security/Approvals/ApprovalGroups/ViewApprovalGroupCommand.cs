using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

public class ViewApprovalGroupCommand : UpdateCommand, IRequest<CommandResult<ApprovalGroupViewModel>>
{
	public string Id { get; set; }
}