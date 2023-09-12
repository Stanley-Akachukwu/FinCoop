using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows
{
    public class GetApprovalWorkflowByIdCommand : IRequest<CommandResult<GetApprovalWorkflowViewModel>>
    {
        public string Id  { get; set; }
    }
}


