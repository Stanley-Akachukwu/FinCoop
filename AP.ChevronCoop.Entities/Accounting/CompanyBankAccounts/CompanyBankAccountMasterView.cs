namespace AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;

public class CompanyBankAccountMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string LedgerAccountId { get; set; }
    public string BankId { get; set; }
    public string? BranchName { get; set; }
    public string? BranchAddress { get; set; }
    public string CurrencyId { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string? BVN { get; set; }
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
    public string? LedgerAccountId_AccountType { get; set; }
    public string? LedgerAccountId_UOM { get; set; }
    public string? LedgerAccountId_CurrencyId { get; set; }
    public string? LedgerAccountId_Code { get; set; }
    public string? LedgerAccountId_Name { get; set; }
    public string? LedgerAccountId_ParentId { get; set; }
    public decimal? LedgerAccountId_ClearedBalance { get; set; }
    public decimal? LedgerAccountId_UnclearedBalance { get; set; }
    public decimal? LedgerAccountId_LedgerBalance { get; set; }
    public decimal? LedgerAccountId_AvailableBalance { get; set; }
    public bool? LedgerAccountId_IsOfficeAccount { get; set; }
    public bool? LedgerAccountId_AllowManualEntry { get; set; }
    public bool? LedgerAccountId_IsClosed { get; set; }
    public DateTime? LedgerAccountId_DateClosed { get; set; }
    public string? LedgerAccountId_ClosedByUserName { get; set; }
    public bool? LedgerAccountId_IsActive { get; set; }
    public string? LedgerAccountId_CreatedByUserId { get; set; }
    public string? LedgerAccountId_UpdatedByUserId { get; set; }
    public string? LedgerAccountId_DeletedByUserId { get; set; }
    public bool? LedgerAccountId_IsDeleted { get; set; }
    public string? LedgerAccountId_Tags { get; set; }
    public string? LedgerAccountId_Caption { get; set; }
    public string? BankId_Code { get; set; }
    public string? BankId_SortCode { get; set; }
    public string? BankId_Name { get; set; }
    public string? BankId_Address { get; set; }
    public string? BankId_ContactName { get; set; }
    public string? BankId_ContactDetails { get; set; }
    public bool? BankId_IsActive { get; set; }
    public string? BankId_CreatedByUserId { get; set; }
    public string? BankId_UpdatedByUserId { get; set; }
    public string? BankId_DeletedByUserId { get; set; }
    public bool? BankId_IsDeleted { get; set; }
    public string? BankId_Tags { get; set; }
    public string? BankId_Caption { get; set; }
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

    public string Details => $"{AccountNumber} - {BankId_Name} ({AccountName})";
}