using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows
{
    public class QueryApprovalWorkflowCommand : IRequest<CommandResult<IQueryable<ApprovalWorkflow>>>
    {
    }
}
