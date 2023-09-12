namespace AP.ChevronCoop.Entities.MasterData.Charges;

public class ChargeMasterView
{
  public long? RowNumber { get; set; }
  public string Id { get; set; }
  public string Code { get; set; }
  public string Name { get; set; }
  public string Method { get; set; }
  public string Target { get; set; }
  public string CalculationMethod { get; set; }
  public string CurrencyId { get; set; }
  public decimal ChargeValue { get; set; }
  public decimal? MaximumCharge { get; set; }
  public decimal? MinimimumCharge { get; set; }
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
  public string? CurrencyId_Code { get; set; }
  public string? CurrencyId_Name { get; set; }
  public string? CurrencyId_Symbol { get; set; }
  public string? CurrencyId_IsoSymbol { get; set; }
  public int? CurrencyId_DecimalPlaces { get; set; }
  public string? CurrencyId_Format { get; set; }
  public bool? CurrencyId_IsActive { get; set; }
  public string? CurrencyId_CreatedByUserId { get; set; }
  public string? CurrencyId_UpdatedByUserId { get; set; }
  public string? CurrencyId_DeletedByUserId { get; set; }
  public bool? CurrencyId_IsDeleted { get; set; }
  public string? CurrencyId_Tags { get; set; }
  public string? CurrencyId_Caption { get; set; }
}