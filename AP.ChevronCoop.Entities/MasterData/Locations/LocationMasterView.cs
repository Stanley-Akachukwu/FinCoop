namespace AP.ChevronCoop.Entities.MasterData.Locations;

public class LocationMasterView
{
  public long? RowNumber { get; set; }
  public string Id { get; set; }
  public string LocationType { get; set; }
  public string? ParentId { get; set; }
  public string Code { get; set; }
  public string Name { get; set; }
  public bool SystemFlag { get; set; }
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
  public string? Header_LocationType { get; set; }
  public string? Header_ParentId { get; set; }
  public string? Header_Code { get; set; }
  public string? Header_Name { get; set; }
  public bool? Header_SystemFlag { get; set; }
  public bool? Header_IsActive { get; set; }
  public string? Header_CreatedByUserId { get; set; }
  public string? Header_UpdatedByUserId { get; set; }
  public string? Header_DeletedByUserId { get; set; }
  public bool? Header_IsDeleted { get; set; }
  public string? Header_Tags { get; set; }
  public string? Header_Caption { get; set; }
  public int? ChildCount { get; set; }
  public bool? HasChildren { get; set; }
}