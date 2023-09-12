using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows
{
    public partial class CreateApprovalWorkflowCommand :  IRequest<CommandResult<ApprovalWorkflowViewModel>>
    {
        public string CreatedByUserId { get; set; }
        public string WorkflowName { get; set; }
        public List<WorkflowApprovalGroupModel> ApprovalGroups { get; set; }
    }
}
