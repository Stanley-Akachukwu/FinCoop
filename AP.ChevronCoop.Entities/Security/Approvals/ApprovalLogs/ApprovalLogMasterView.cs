namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
public class ApprovalLogMasterView
{
  public long? RowNumber { get; set; }

  public string Id { get; set; }

  public string ApprovalId { get; set; }


  public int Sequence { get; set; }

  public string ApprovalGroupId { get; set; }

  public string ApprovedByUserId { get; set; }


  public DateTime DateApproved { get; set; }

  public string Status { get; set; }

  public string Comment { get; set; }

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

  public string ApprovalId_Module { get; set; }

  public string ApprovalId_ApprovalType { get; set; }

  public string ApprovalId_Status { get; set; }

  public int? ApprovalId_CurrentSequence { get; set; }

  public string ApprovalId_ApprovalWorkflowId { get; set; }

  public string ApprovalId_Payload { get; set; }

  public bool? ApprovalId_IsApprovalCompleted { get; set; }

  public string ApprovalId_Comment { get; set; }

  public string ApprovalId_EntityId { get; set; }

  public int? ApprovalId_TriedCount { get; set; }

  public bool? ApprovalId_IsActive { get; set; }

  public string ApprovalId_CreatedByUserId { get; set; }

  public string ApprovalId_UpdatedByUserId { get; set; }

  public string ApprovalId_DeletedByUserId { get; set; }

  public bool? ApprovalId_IsDeleted { get; set; }

  public string ApprovalId_Tags { get; set; }

  public string ApprovalId_Caption { get; set; }

  public string ApprovalGroupId_ApprovalWorkflowId { get; set; }

  public string ApprovalGroupId_Name { get; set; }

  public bool? ApprovalGroupId_IsActive { get; set; }

  public string ApprovalGroupId_CreatedByUserId { get; set; }

  public string ApprovalGroupId_UpdatedByUserId { get; set; }

  public string ApprovalGroupId_DeletedByUserId { get; set; }

  public bool? ApprovalGroupId_IsDeleted { get; set; }

  public string ApprovalGroupId_Tags { get; set; }

  public string ApprovalGroupId_Caption { get; set; }

  public string ApprovedByUserId_AdObjectId { get; set; }

  public bool? ApprovedByUserId_IsAdmin { get; set; }

  public string ApprovedByUserId_SecondaryPhone { get; set; }

  public bool? ApprovedByUserId_SecondaryPhoneConfirmed { get; set; }

  public string ApprovedByUserId_UserName { get; set; }

  public string ApprovedByUserId_NormalizedUserName { get; set; }

  public string ApprovedByUserId_Email { get; set; }

  public string ApprovedByUserId_NormalizedEmail { get; set; }

  public bool? ApprovedByUserId_EmailConfirmed { get; set; }

  public string ApprovedByUserId_PasswordHash { get; set; }

  public string ApprovedByUserId_SecurityStamp { get; set; }

  public string ApprovedByUserId_ConcurrencyStamp { get; set; }

  public string ApprovedByUserId_PhoneNumber { get; set; }

  public bool? ApprovedByUserId_PhoneNumberConfirmed { get; set; }

  public bool? ApprovedByUserId_TwoFactorEnabled { get; set; }

  public DateTimeOffset? ApprovedByUserId_LockoutEnd { get; set; }

  public bool? ApprovedByUserId_LockoutEnabled { get; set; }

  public int? ApprovedByUserId_AccessFailedCount { get; set; }
}