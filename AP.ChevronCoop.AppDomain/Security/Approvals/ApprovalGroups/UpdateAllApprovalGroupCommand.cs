using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

public class UpdateAllApprovalGroupCommand : UpdateCommand, IRequest<CommandResult<string>>
{
	public List<ApprovalWorkflowViewModel> ApprovalWorkflowViewModels { get; set; }
}