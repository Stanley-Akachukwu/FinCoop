
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory
{
    public interface IApprovalDetailFactory
    {
        Task ProcessProviderApprovalDetails(Approval approval);
        Task<string> ProcessDetail(ApprovalType type,  string approvalId);
       
    }
}
