using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Security.Approvals
{
    public partial class ApprovalMasterView
    {

        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string Module { get; set; }
        public string Payload { get; set; }
        public string? ApprovalViewModelPayload { get; set; }
        public string? ApprovalType { get; set; }
        public string? Comment { get; set; }
        public string? ApprovalWorkflowId { get; set; }
        // public bool IsApproved { get; set; }
        // public string EntityId { get; set; }
        // public string EventGlobalCodeId { get; set; }
        // public string RequestedByUserId { get; set; }
        // public string ProcessedByUserId { get; set; }
        // public DateTimeOffset? RequestDate { get; set; }
        // public DateTimeOffset? ProcessedDate { get; set; }
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
        // public int CurrentApprovalState { get; set; }
        // public bool IsApprovalCompleted { get; set; }
        // public string? RequestEntityId { get; set; }
        // public string EmailTitle { get; set; }
        // public string EventGlobalCodeId_CodeType { get; set; }
        // public string EventGlobalCodeId_Code { get; set; }
        // public string EventGlobalCodeId_Name { get; set; }
        // public bool? EventGlobalCodeId_SystemFlag { get; set; }
        // public bool? EventGlobalCodeId_IsActive { get; set; }
        // public string EventGlobalCodeId_CreatedByUserId { get; set; }
        // public string EventGlobalCodeId_UpdatedByUserId { get; set; }
        // public string EventGlobalCodeId_DeletedByUserId { get; set; }
        // public bool? EventGlobalCodeId_IsDeleted { get; set; }
        // public string EventGlobalCodeId_Tags { get; set; }
        // public string EventGlobalCodeId_Caption { get; set; }
    }
}

