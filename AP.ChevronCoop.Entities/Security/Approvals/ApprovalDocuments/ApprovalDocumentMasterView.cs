using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalDocuments
{
    public partial class ApprovalDocumentMasterView
    {

        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string ApprovalId { get; set; }
        public byte[] Evidence { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string Description { get; set; }
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
        public string ApprovalId_EventGlobalCodeId { get; set; }
        public string ApprovalId_Module { get; set; }
        public string ApprovalId_Payload { get; set; }
        public string ApprovalId_EntityPageUrl { get; set; }
        public string ApprovalId_EntityType { get; set; }
        public string ApprovalId_EntityId { get; set; }
        public string ApprovalId_TableName { get; set; }
        public bool? ApprovalId_IsApproved { get; set; }
        public string ApprovalId_RequestedByUserId { get; set; }
        public DateTimeOffset? ApprovalId_RequestDate { get; set; }
        public string ApprovalId_ProcessedByUserId { get; set; }
        public DateTimeOffset? ApprovalId_ProcessedDate { get; set; }
        public bool? ApprovalId_IsActive { get; set; }
        public string ApprovalId_CreatedByUserId { get; set; }
        public string ApprovalId_UpdatedByUserId { get; set; }
        public string ApprovalId_DeletedByUserId { get; set; }
        public bool? ApprovalId_IsDeleted { get; set; }
        public string ApprovalId_Tags { get; set; }
        public string ApprovalId_Caption { get; set; }
    }
}