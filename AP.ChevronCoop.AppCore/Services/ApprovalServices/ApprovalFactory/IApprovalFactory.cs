using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public interface IApprovalFactory
{
    Task<CommandResult<bool>> Initiate(Approval request);
    Task<CommandResult<bool>> Process(Approval request, string? approvedById, string? comment, ApprovalStatus status=ApprovalStatus.PENDING_APPROVAL);
    
}