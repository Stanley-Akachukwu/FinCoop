using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows
{
    public class WorkflowApprovalGroupModel
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public int RequiredApprovers { get; set; }
        
        [Required]
        public int ApprovalSequence { get; set; }
    }
}
