using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows
{
    public class ApprovalWorkflow : BaseEntity<string>
    {
        public ApprovalWorkflow()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
            ApprovalGroups = new List<ApprovalGroup>();

        }
        public string WorkflowName { get; set; }
		public bool IsDefaultApprovalWorkflow { get; set; } 
        public int RequiredApprovers { get; set; }
        public int RequiredGroups { get; set; }
        public virtual List<ApprovalGroup> ApprovalGroups { get; set; }
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
