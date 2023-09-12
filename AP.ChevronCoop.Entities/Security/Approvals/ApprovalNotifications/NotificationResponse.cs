using System;
namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications
{
	public class NotificationResponse
	{
		public string ApprovalNotificationId { get; set; }
        public string Payload { get; set; }
        public string ApprovalWorkflowId { get; set; }
        public string Reminder { get; set; }
        public string Excalation { get; set; }
    }
}

