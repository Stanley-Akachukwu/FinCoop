using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;

public class SendApprovalRequestNotificationCommand : IRequest<bool>
{
  public string ApprovalId { get; set; }
  public int Sequence { get; set; } = 0;
}