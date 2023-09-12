using AP.ChevronCoop.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads
{
    public partial class MemberBulkUploadSessionMasterView 
    {
        public string Description { get; set; }
        [ConcurrencyCheck]
        public Guid RowVersion { get; set; }
        public string FullText { get; set; }
        public string Tags { get; set; }
        public string? Caption { get; set; }  
        public string MemberBulkUploadSessionId { get; set; }
        public string ApprovedByUserId { get; set; }
        public int Size { get; set; }
        public string ApprovalWorkflowId { get; set; }
        public string Status { get; set; }
        public string ApprovalId { get; set; }
        public string UserId_UserName { get; set; }
        public bool ApprovalId_ApprovalStatus { get; set; }
        public string ApprovalId_Id { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; } = DateTime.Now;
        public string UpdatedByUserId { get; set; }
        public DateTimeOffset? DateUpdated { get; set; } = DateTime.Now;
        public string DeletedByUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
