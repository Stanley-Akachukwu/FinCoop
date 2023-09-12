using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;

public class SendApprovalNotificationCommand : IRequest<bool>
{
  public string ApprovalId { get; set; }
  public bool IsApproved { get; set; }
}