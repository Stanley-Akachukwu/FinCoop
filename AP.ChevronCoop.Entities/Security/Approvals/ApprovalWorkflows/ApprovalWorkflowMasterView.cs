

using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows
{
    public partial class ApprovalWorkflowMasterView
    {
        public string Id { get; set; }

        [ConcurrencyCheck]
        public long? RowNumber { get; set; }
        public string? WorkflowName { get; set; }
        public string? Description { get; set; }
        public int RequiredApprovers { get; set; }
        public int RequiredGroups { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public string? UpdatedByUserId { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public Guid? RowVersion { get; set; }
        public bool IsDefaultApprovalWorkflow { get; set; }
    }
}
 