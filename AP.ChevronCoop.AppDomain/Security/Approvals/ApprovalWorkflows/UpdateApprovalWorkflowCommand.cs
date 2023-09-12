using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows
{
    public partial class UpdateApprovalWorkflowCommand : IRequest<CommandResult<ApprovalWorkflowViewModel>>
    {
        public string Id { get; set; }
        public string UpdatedByUserId { get; set; }
        public string WorkflowName { get; set; }
        public List<WorkflowApprovalGroupModel> ApprovalGroups { get; set; }

    }
}
