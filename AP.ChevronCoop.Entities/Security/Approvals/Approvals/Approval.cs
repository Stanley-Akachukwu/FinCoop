using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;

namespace AP.ChevronCoop.Entities.Security.Approvals.Approvals
{
    public class Approval : BaseEntity<string>
    {

        public Approval()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string Module { get; set; }
        public ApprovalType ApprovalType { get; set; }
        public ApprovalStatus Status { get; set; }
        public int CurrentSequence { get; set; }
        public string? ApprovalWorkflowId { get; set; }
        public ApprovalWorkflow? ApprovalWorkflow { get; set; }
        public string Payload { get; set; }
        public string? ApprovalViewModelPayload { get; set; }
        public bool IsApprovalCompleted { get; set; }
        public string? Comment { get; set; }
        public string EntityId { get; set; }
        public int TriedCount { get; set; }

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
