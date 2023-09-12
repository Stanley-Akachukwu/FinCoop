using System;
namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications
{
	public class ApprovalReminder
	{
        public int? ReminderTriggerTime { get; set; }
        public int? ReminderCount { get; set; }
		public List<string> ReminderUserIds { get; set; } = new();
	}
}

