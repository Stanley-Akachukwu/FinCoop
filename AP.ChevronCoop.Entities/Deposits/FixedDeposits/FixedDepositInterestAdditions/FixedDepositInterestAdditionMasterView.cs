namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;

public class FixedDepositInterestAdditionMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string? FixedDepositAccountId { get; set; }
    public string? InterestScheduleItemId { get; set; }
    public decimal InterestEarned { get; set; }
    public string? TransactionJournalId { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string Status { get; set; }
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
    public string? FixedDepositAccountId_ApplicationId { get; set; }
    public string? FixedDepositAccountId_AccountNo { get; set; }
    public string? FixedDepositAccountId_CustomerId { get; set; }
    public string? FixedDepositAccountId_DepositProductId { get; set; }
    public string? FixedDepositAccountId_DepositAccountId { get; set; }
    public string? FixedDepositAccountId_ChargesAccruedAccountId { get; set; }
    public string? FixedDepositAccountId_ChargesIncomeAccountId { get; set; }
    public string? FixedDepositAccountId_InterestEarnedAccountId { get; set; }
    public string? FixedDepositAccountId_InterestPayoutAccountId { get; set; }
    public string? FixedDepositAccountId_ChargesWaivedAccountId { get; set; }
    public decimal? FixedDepositAccountId_Amount { get; set; }
    public string? FixedDepositAccountId_TenureUnit { get; set; }
    public decimal? FixedDepositAccountId_TenureValue { get; set; }
    public decimal? FixedDepositAccountId_InterestRate { get; set; }
    public string? FixedDepositAccountId_MaturityInstructionType { get; set; }
    public string? FixedDepositAccountId_LiquidationAccountType { get; set; }
    public string? FixedDepositAccountId_SavingsLiquidationAccountId { get; set; }
    public string? FixedDepositAccountId_SpecialDepositLiquidationAccountId { get; set; }
    public string? FixedDepositAccountId_CustomerBankLiquidationAccountId { get; set; }
    public DateTime? FixedDepositAccountId_LastInterestComputationDate { get; set; }
    public bool? FixedDepositAccountId_HasMature { get; set; }
    public bool? FixedDepositAccountId_IsClosed { get; set; }
    public DateTime? FixedDepositAccountId_DateClosed { get; set; }
    public string? FixedDepositAccountId_ClosedByUserId { get; set; }
    public decimal? FixedDepositAccountId_MaximumBalanceLimit { get; set; }
    public decimal? FixedDepositAccountId_MinimumBalanceLimit { get; set; }
    public decimal? FixedDepositAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? FixedDepositAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? FixedDepositAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? FixedDepositAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? FixedDepositAccountId_IsActive { get; set; }
    public string? FixedDepositAccountId_CreatedByUserId { get; set; }
    public string? FixedDepositAccountId_UpdatedByUserId { get; set; }
    public string? FixedDepositAccountId_DeletedByUserId { get; set; }
    public bool? FixedDepositAccountId_IsDeleted { get; set; }
    public string? FixedDepositAccountId_Tags { get; set; }
    public string? FixedDepositAccountId_Caption { get; set; }
    public string? FixedDepositAccountId_ParentAccountId { get; set; }
    public string? FixedDepositAccountId_RootParentAccountId { get; set; }
    public string? InterestScheduleItemId_FixedDepositAccountId { get; set; }
    public string? InterestScheduleItemId_FixedDepositInterestScheduleId { get; set; }
    public decimal? InterestScheduleItemId_OldBalance { get; set; }
    public decimal? InterestScheduleItemId_PeriodCashAddition { get; set; }
    public decimal? InterestScheduleItemId_InterestRate { get; set; }
    public decimal? InterestScheduleItemId_InterestEarned { get; set; }
    public decimal? InterestScheduleItemId_NewBalance { get; set; }
    public bool? InterestScheduleItemId_IsActive { get; set; }
    public string? InterestScheduleItemId_CreatedByUserId { get; set; }
    public string? InterestScheduleItemId_UpdatedByUserId { get; set; }
    public string? InterestScheduleItemId_DeletedByUserId { get; set; }
    public bool? InterestScheduleItemId_IsDeleted { get; set; }
    public string? InterestScheduleItemId_Tags { get; set; }
    public string? InterestScheduleItemId_Caption { get; set; }
    public string? TransactionJournalId_TransactionNo { get; set; }
    public string? TransactionJournalId_Title { get; set; }
    public string? TransactionJournalId_TransactionType { get; set; }
    public string? TransactionJournalId_DocumentRef { get; set; }
    public string? TransactionJournalId_DocumentRefId { get; set; }
    public string? TransactionJournalId_PostingRef { get; set; }
    public string? TransactionJournalId_PostingRefId { get; set; }
    public string? TransactionJournalId_EntityRef { get; set; }
    public string? TransactionJournalId_EntityRefId { get; set; }
    public DateTime? TransactionJournalId_TransactionDate { get; set; }
    public bool? TransactionJournalId_IsPosted { get; set; }
    public string? TransactionJournalId_PostedByUserId { get; set; }
    public DateTime? TransactionJournalId_DatePosted { get; set; }
    public bool? TransactionJournalId_IsReversed { get; set; }
    public string? TransactionJournalId_ReversedByUserId { get; set; }
    public DateTime? TransactionJournalId_ReversalDate { get; set; }
    public string? TransactionJournalId_ApprovalId { get; set; }
    public DateTime? TransactionJournalId_ApprovalDate { get; set; }
    public string? TransactionJournalId_Memo { get; set; }
    public bool? TransactionJournalId_IsActive { get; set; }
    public string? TransactionJournalId_CreatedByUserId { get; set; }
    public string? TransactionJournalId_UpdatedByUserId { get; set; }
    public string? TransactionJournalId_DeletedByUserId { get; set; }
    public bool? TransactionJournalId_IsDeleted { get; set; }
    public string? TransactionJournalId_Tags { get; set; }
    public string? TransactionJournalId_Caption { get; set; }
}