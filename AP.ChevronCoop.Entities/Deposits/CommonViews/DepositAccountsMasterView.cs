 

namespace AP.ChevronCoop.Entities.Deposits.DepositApplications
{
    public partial class DepositAccountsMasterView
    {
        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string ApplicationId { get; set; }
        public string CustomerId { get; set; }
        public string DepositProductId { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string LedgerAccountId { get; set; }
        public decimal AvailableBalance { get; set; }
        public  decimal LedgerBalance { get; set; }
        public decimal Amount { get; set; }

        public string Status { get; set; }
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
        public decimal? MonthlyContributionAmount { get; set; }

        public string? ChargesAccruedAccountId_AccountType { get; set; }
        public string? ChargesAccruedAccountId_UOM { get; set; }
        public string? ChargesAccruedAccountId_CurrencyId { get; set; }
        public string? ChargesAccruedAccountId_Code { get; set; }
        public string? ChargesAccruedAccountId_Name { get; set; }
        public string? ChargesAccruedAccountId_ParentId { get; set; }
        public decimal? ChargesAccruedAccountId_ClearedBalance { get; set; }
        public decimal? ChargesAccruedAccountId_UnclearedBalance { get; set; }
        public decimal? ChargesAccruedAccountId_LedgerBalance { get; set; }
        public decimal? ChargesAccruedAccountId_AvailableBalance { get; set; }
        public bool? ChargesAccruedAccountId_IsOfficeAccount { get; set; }
        public bool? ChargesAccruedAccountId_AllowManualEntry { get; set; }
        public bool? ChargesAccruedAccountId_IsClosed { get; set; }
        public DateTime? ChargesAccruedAccountId_DateClosed { get; set; }
        public string? ChargesAccruedAccountId_ClosedByUserName { get; set; }
        public bool? ChargesAccruedAccountId_IsActive { get; set; }
        public string? ChargesAccruedAccountId_CreatedByUserId { get; set; }
        public string? ChargesAccruedAccountId_UpdatedByUserId { get; set; }
        public string? ChargesAccruedAccountId_DeletedByUserId { get; set; }
        public bool? ChargesAccruedAccountId_IsDeleted { get; set; }
        public string? ChargesAccruedAccountId_Tags { get; set; }
        public string? ChargesAccruedAccountId_Caption { get; set; }
        public string? ChargesIncomeAccountId_AccountType { get; set; }
        public string? ChargesIncomeAccountId_UOM { get; set; }
        public string? ChargesIncomeAccountId_CurrencyId { get; set; }
        public string? ChargesIncomeAccountId_Code { get; set; }
        public string? ChargesIncomeAccountId_Name { get; set; }
        public string? ChargesIncomeAccountId_ParentId { get; set; }
        public decimal? ChargesIncomeAccountId_ClearedBalance { get; set; }
        public decimal? ChargesIncomeAccountId_UnclearedBalance { get; set; }
        public decimal? ChargesIncomeAccountId_LedgerBalance { get; set; }
        public decimal? ChargesIncomeAccountId_AvailableBalance { get; set; }
        public bool? ChargesIncomeAccountId_IsOfficeAccount { get; set; }
        public bool? ChargesIncomeAccountId_AllowManualEntry { get; set; }
        public bool? ChargesIncomeAccountId_IsClosed { get; set; }
        public DateTime? ChargesIncomeAccountId_DateClosed { get; set; }
        public string? ChargesIncomeAccountId_ClosedByUserName { get; set; }
        public bool? ChargesIncomeAccountId_IsActive { get; set; }
        public string? ChargesIncomeAccountId_CreatedByUserId { get; set; }
        public string? ChargesIncomeAccountId_UpdatedByUserId { get; set; }
        public string? ChargesIncomeAccountId_DeletedByUserId { get; set; }
        public bool? ChargesIncomeAccountId_IsDeleted { get; set; }
        public string? ChargesIncomeAccountId_Tags { get; set; }
        public string? ChargesIncomeAccountId_Caption { get; set; }
        public string? InterestEarnedAccountId_AccountType { get; set; }
        public string? InterestEarnedAccountId_UOM { get; set; }
        public string? InterestEarnedAccountId_CurrencyId { get; set; }
        public string? InterestEarnedAccountId_Code { get; set; }
        public string? InterestEarnedAccountId_Name { get; set; }
        public string? InterestEarnedAccountId_ParentId { get; set; }
        public decimal? InterestEarnedAccountId_ClearedBalance { get; set; }
        public decimal? InterestEarnedAccountId_UnclearedBalance { get; set; }
        public decimal? InterestEarnedAccountId_LedgerBalance { get; set; }
        public decimal? InterestEarnedAccountId_AvailableBalance { get; set; }
        public bool? InterestEarnedAccountId_IsOfficeAccount { get; set; }
        public bool? InterestEarnedAccountId_AllowManualEntry { get; set; }
        public bool? InterestEarnedAccountId_IsClosed { get; set; }
        public DateTime? InterestEarnedAccountId_DateClosed { get; set; }
        public string? InterestEarnedAccountId_ClosedByUserName { get; set; }
        public bool? InterestEarnedAccountId_IsActive { get; set; }
        public string? InterestEarnedAccountId_CreatedByUserId { get; set; }
        public string? InterestEarnedAccountId_UpdatedByUserId { get; set; }
        public string? InterestEarnedAccountId_DeletedByUserId { get; set; }
        public bool? InterestEarnedAccountId_IsDeleted { get; set; }
        public string? InterestEarnedAccountId_Tags { get; set; }
        public string? InterestEarnedAccountId_Caption { get; set; }

     
        public string? InterestPayoutAccountId_AccountType { get; set; }
        public string? InterestPayoutAccountId_UOM { get; set; }
        public string? InterestPayoutAccountId_CurrencyId { get; set; }
        public string? InterestPayoutAccountId_Code { get; set; }
        public string? InterestPayoutAccountId_Name { get; set; }
        public string? InterestPayoutAccountId_ParentId { get; set; }
        public decimal? InterestPayoutAccountId_ClearedBalance { get; set; }
        public decimal? InterestPayoutAccountId_UnclearedBalance { get; set; }
        public decimal? InterestPayoutAccountId_LedgerBalance { get; set; }
        public decimal? InterestPayoutAccountId_AvailableBalance { get; set; }
        public bool? InterestPayoutAccountId_IsOfficeAccount { get; set; }
        public bool? InterestPayoutAccountId_AllowManualEntry { get; set; }
        public bool? InterestPayoutAccountId_IsClosed { get; set; }
        public DateTime? InterestPayoutAccountId_DateClosed { get; set; }
        public string? InterestPayoutAccountId_ClosedByUserName { get; set; }
        public bool? InterestPayoutAccountId_IsActive { get; set; }
        public string? InterestPayoutAccountId_CreatedByUserId { get; set; }
        public string? InterestPayoutAccountId_UpdatedByUserId { get; set; }
        public string? InterestPayoutAccountId_DeletedByUserId { get; set; }
        public bool? InterestPayoutAccountId_IsDeleted { get; set; }
        public string? InterestPayoutAccountId_Tags { get; set; }
        public string? InterestPayoutAccountId_Caption { get; set; }


        public string? ChargesWaivedAccountId_AccountType { get; set; }
        public string? ChargesWaivedAccountId_UOM { get; set; }
        public string? ChargesWaivedAccountId_CurrencyId { get; set; }
        public string? ChargesWaivedAccountId_Code { get; set; }
        public string? ChargesWaivedAccountId_Name { get; set; }
        public string? ChargesWaivedAccountId_ParentId { get; set; }
        public decimal? ChargesWaivedAccountId_ClearedBalance { get; set; }
        public decimal? ChargesWaivedAccountId_UnclearedBalance { get; set; }
        public decimal? ChargesWaivedAccountId_LedgerBalance { get; set; }
        public decimal? ChargesWaivedAccountId_AvailableBalance { get; set; }
        public bool? ChargesWaivedAccountId_IsOfficeAccount { get; set; }
        public bool? ChargesWaivedAccountId_AllowManualEntry { get; set; }
        public bool? ChargesWaivedAccountId_IsClosed { get; set; }
        public DateTime? ChargesWaivedAccountId_DateClosed { get; set; }
        public string? ChargesWaivedAccountId_ClosedByUserName { get; set; }
        public bool? ChargesWaivedAccountId_IsActive { get; set; }
        public string? ChargesWaivedAccountId_CreatedByUserId { get; set; }
        public string? ChargesWaivedAccountId_UpdatedByUserId { get; set; }
        public string? ChargesWaivedAccountId_DeletedByUserId { get; set; }
        public bool? ChargesWaivedAccountId_IsDeleted { get; set; }
        public string? ChargesWaivedAccountId_Tags { get; set; }
        public string? ChargesWaivedAccountId_Caption { get; set; }

    }
}

 