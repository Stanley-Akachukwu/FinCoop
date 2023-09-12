namespace AP.ChevronCoop.Entities.Security.Approvals.Approvals;

public class ApprovalView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string Module { get; set; }
    public string? ApprovalType { get; set; }
    public string? Comment { get; set; }
    
    public int CurrentSequence { get; set; } 
    public bool IsApprovalCompleted { get; set; } 
    public string? EntityId { get; set; } 
    public int TriedCount { get; set; } 
    public string? ApprovalWorkflowId { get; set; }
    public string Description { get; set; }
    public string? Status { get; set; }
    public bool IsActive { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
    public string UpdatedByUserId { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public string DeletedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
    public Guid RowVersion { get; set; }
    public string FullText { get; set; }
    public string Tags { get; set; }
    public string Caption { get; set; }
}