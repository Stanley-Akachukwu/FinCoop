namespace AP.ChevronCoop.Entities.MasterData.Currencies;

public class CurrencyMasterView
{
  public long? RowNumber { get; set; }
  public string Id { get; set; }
  public string Code { get; set; }
  public string Name { get; set; }
  public string Symbol { get; set; }
  public string? IsoSymbol { get; set; }
  public int DecimalPlaces { get; set; }
  public string? Format { get; set; }
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
}