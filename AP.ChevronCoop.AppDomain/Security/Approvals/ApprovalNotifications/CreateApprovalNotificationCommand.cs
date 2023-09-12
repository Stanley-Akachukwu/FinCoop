using AP.ChevronCoop.Commons;
using MediatR;


namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications
{
    public partial class CreateApprovalNotificationCommand : IRequest<CommandResult<string>>
    {
        public string ApprovalWorkflowId { get; set; }
        public int? ReminderTriggerTime { get; set; }
        public int? MaxReminderCount { get; set; } //Set to trigger Escalation 
        public List<string> ExcludeFromReminderUserIds { get; set; }
        public List<string> EscalateToUserIds { get; set; }
        public string CreatedByUserId { get; set; } 

    }

}
