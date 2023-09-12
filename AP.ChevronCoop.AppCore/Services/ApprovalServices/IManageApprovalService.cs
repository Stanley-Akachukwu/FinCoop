using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices
{
    public interface IManageApprovalService
    {
        Task<CommandResult<Approval>> CreateApproval(CreateApprovalModel request, bool useDefaultApprovalFlow,
            string? approvalWorkFlow = null);
        Task<CommandResult<string>> HandleApproval(HandleApprovalCommand request);

    }
}
