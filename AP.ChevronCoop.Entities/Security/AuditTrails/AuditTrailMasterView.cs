namespace AP.ChevronCoop.Entities.Security.AuditTrails;

public class AuditTrailMasterView
{
  public long? RowNumber { get; set; }
  public string Id { get; set; }
  public string ApplicationUserId { get; set; }
  public DateTime Timestamp { get; set; }
  public string EventType { get; set; }
  public string TableName { get; set; }
  public string PrimaryKey { get; set; }
  public string OldValues { get; set; }
  public string NewValues { get; set; }
  public string AuditJson { get; set; }
  public string? Module { get; set; }
  public string? IPAddress { get; set; }
  public string? Action { get; set; }
  public string? Description { get; set; }
  public bool IsActive { get; set; }
  public string? CreatedByUserId { get; set; }
  public DateTimeOffset? DateCreated { get; set; }
  public string? UpdatedByUserId { get; set; }
  public DateTimeOffset? DateUpdated { get; set; }
  public string? DeletedByUserId { get; set; }
  public bool IsDeleted { get; set; }
  public DateTimeOffset? DateDeleted { get; set; }
  public Guid RowVersion { get; set; }
  public string? FullText { get; set; }
  public string? Tags { get; set; }
  public string? Caption { get; set; }
  public string? ApplicationUserId_UserName { get; set; }
  public string? ApplicationUserId_Email { get; set; }
  public bool? ApplicationUserId_EmailConfirmed { get; set; }
  public string? ApplicationUserId_PhoneNumber { get; set; }
  public bool? ApplicationUserId_PhoneNumberConfirmed { get; set; }
  public bool? ApplicationUserId_TwoFactorEnabled { get; set; }
  public DateTimeOffset? ApplicationUserId_LockoutEnd { get; set; }
  public bool? ApplicationUserId_LockoutEnabled { get; set; }
  public int? ApplicationUserId_AccessFailedCount { get; set; }
}