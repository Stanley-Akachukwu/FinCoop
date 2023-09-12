using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;


public partial class ApprovalGroupMemberMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string ApprovalGroupId { get; set; }
    public int ApprovalSequence { get; set; }
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
    public string ApplicationUserId { get; set; }
    public string ApprovalGroupId_ApprovalWorkflowId { get; set; }
    public string ApprovalGroupId_Name { get; set; }
    public bool? ApprovalGroupId_IsActive { get; set; }
    public string ApprovalGroupId_CreatedByUserId { get; set; }
    public string ApprovalGroupId_UpdatedByUserId { get; set; }
    public string ApprovalGroupId_DeletedByUserId { get; set; }
    public bool? ApprovalGroupId_IsDeleted { get; set; }
    public string ApprovalGroupId_Tags { get; set; }
    public string ApprovalGroupId_Caption { get; set; }
    public string ApplicationUserId_AdObjectId { get; set; }
    public bool? ApplicationUserId_IsAdmin { get; set; }
    public string ApplicationUserId_SecondaryPhone { get; set; }
    public bool? ApplicationUserId_SecondaryPhoneConfirmed { get; set; }
    public string ApplicationUserId_UserName { get; set; }
    public string ApplicationUserId_NormalizedUserName { get; set; }
    public string ApplicationUserId_Email { get; set; }
    public string ApplicationUserId_NormalizedEmail { get; set; }
    public bool? ApplicationUserId_EmailConfirmed { get; set; }
    public string ApplicationUserId_PasswordHash { get; set; }
    public string ApplicationUserId_SecurityStamp { get; set; }
    public string ApplicationUserId_ConcurrencyStamp { get; set; }
    public string ApplicationUserId_PhoneNumber { get; set; }
    public bool? ApplicationUserId_PhoneNumberConfirmed { get; set; }
    public bool? ApplicationUserId_TwoFactorEnabled { get; set; }
    public DateTimeOffset? ApplicationUserId_LockoutEnd { get; set; }
    public bool? ApplicationUserId_LockoutEnabled { get; set; }
    public int? ApplicationUserId_AccessFailedCount { get; set; }

}

