using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalNotifications
{
    public class ApprovalNotification : BaseEntity<string>
    {
        public ApprovalNotification()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string ApprovalWorkflowId { get; set; }
        public ApprovalWorkflow ApprovalWorkflow { get; set; }
        public string Reminder { get; set; }
        public string Escalation { get; set; }
        public override string DisplayCaption
        {
            get
            {
                return "";
            }
        }
        public override string DropdownCaption
        {
            get
            {
                return "";
            }
        }
        public override string ShortCaption
        {
            get
            {
                return "";
            }
        }
    }
}
