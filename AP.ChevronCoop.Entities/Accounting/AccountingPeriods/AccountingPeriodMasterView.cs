namespace AP.ChevronCoop.Entities.Accounting.AccountingPeriods;

public class AccountingPeriodMasterView
{
    public long? RowNumber { get; set; } 
    public string Id { get; set; } 
    public string CalendarId { get; set; } 
    public string? Name { get; set; } 
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public bool IsCurrent { get; set; } 
    public bool IsClosed { get; set; } 
    public string? ClosedByUserName { get; set; } 
    public DateTime? DateClosed { get; set; } 
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
    public string? CalendarId_Code { get; set; } 
    public string? CalendarId_Name { get; set; } 
    public DateTime? CalendarId_StartDate { get; set; } 
    public DateTime? CalendarId_EndDate { get; set; } 
    public bool? CalendarId_IsCurrent { get; set; } 
    public bool? CalendarId_IsClosed { get; set; } 
    public string? CalendarId_ClosedByUserName { get; set; } 
    public DateTime? CalendarId_DateClosed { get; set; } 
    public bool? CalendarId_IsActive { get; set; } 
    public string? CalendarId_CreatedByUserId { get; set; } 
    public string? CalendarId_UpdatedByUserId { get; set; } 
    public string? CalendarId_DeletedByUserId { get; set; } 
    public bool? CalendarId_IsDeleted { get; set; } 
    public string? CalendarId_Tags { get; set; } 
    public string? CalendarId_Caption { get; set; } 
}