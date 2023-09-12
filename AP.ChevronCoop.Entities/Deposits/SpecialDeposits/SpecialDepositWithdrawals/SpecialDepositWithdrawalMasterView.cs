namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;

public class SpecialDepositWithdrawalMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string? SpecialDepositSourceAccountId { get; set; }
    public decimal Amount { get; set; }
    public string WithdrawalDestinationType { get; set; }
    public string? CustomerDestinationBankAccountId { get; set; }
    public string? TransactionJournalId { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string Status { get; set; }
    public string? ApprovalId { get; set; }
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
    public string? SpecialDepositSourceAccountId_ApplicationId { get; set; }
    public string? SpecialDepositSourceAccountId_AccountNo { get; set; }
    public string? SpecialDepositSourceAccountId_CustomerId { get; set; }
    public string? SpecialDepositSourceAccountId_DepositProductId { get; set; }
    public string? SpecialDepositSourceAccountId_DepositAccountId { get; set; }
    public string? SpecialDepositSourceAccountId_ChargesAccruedAccountId { get; set; }
    public string? SpecialDepositSourceAccountId_ChargesIncomeAccountId { get; set; }
    public string? SpecialDepositSourceAccountId_ChargesWaivedAccountId { get; set; }
    public string? SpecialDepositSourceAccountId_InterestEarnedAccountId { get; set; }
    public string? SpecialDepositSourceAccountId_InterestPayoutAccountId { get; set; }
    public decimal? SpecialDepositSourceAccountId_FundingAmount { get; set; }
    public decimal? SpecialDepositSourceAccountId_InterestRate { get; set; }
    public DateTime? SpecialDepositSourceAccountId_LastInterestComputationDate { get; set; }
    public decimal? SpecialDepositSourceAccountId_MaximumBalanceLimit { get; set; }
    public decimal? SpecialDepositSourceAccountId_MinimumBalanceLimit { get; set; }
    public decimal? SpecialDepositSourceAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? SpecialDepositSourceAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositSourceAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositSourceAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? SpecialDepositSourceAccountId_IsClosed { get; set; }
    public DateTime? SpecialDepositSourceAccountId_DateClosed { get; set; }
    public string? SpecialDepositSourceAccountId_ClosedByUserId { get; set; }
    public bool? SpecialDepositSourceAccountId_IsActive { get; set; }
    public string? SpecialDepositSourceAccountId_CreatedByUserId { get; set; }
    public string? SpecialDepositSourceAccountId_UpdatedByUserId { get; set; }
    public string? SpecialDepositSourceAccountId_DeletedByUserId { get; set; }
    public bool? SpecialDepositSourceAccountId_IsDeleted { get; set; }
    public string? SpecialDepositSourceAccountId_Tags { get; set; }
    public string? SpecialDepositSourceAccountId_Caption { get; set; }
    public string? CustomerDestinationBankAccountId_LedgerAccountId { get; set; }
    public string? CustomerDestinationBankAccountId_CustomerId { get; set; }
    public string? CustomerDestinationBankAccountId_BankId { get; set; }
    public string? CustomerDestinationBankAccountId_AccountName { get; set; }
    public string? CustomerDestinationBankAccountId_AccountNumber { get; set; }
    public string? CustomerDestinationBankAccountId_BVN { get; set; }
    public string? CustomerDestinationBankAccountId_Branch { get; set; }
    public bool? CustomerDestinationBankAccountId_IsActive { get; set; }
    public string? CustomerDestinationBankAccountId_CreatedByUserId { get; set; }
    public string? CustomerDestinationBankAccountId_UpdatedByUserId { get; set; }
    public string? CustomerDestinationBankAccountId_DeletedByUserId { get; set; }
    public bool? CustomerDestinationBankAccountId_IsDeleted { get; set; }
    public string? CustomerDestinationBankAccountId_Tags { get; set; }
    public string? CustomerDestinationBankAccountId_Caption { get; set; }
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
    public string? ApprovalId_Module { get; set; }
    public string? ApprovalId_ApprovalType { get; set; }
    public string? ApprovalId_Status { get; set; }
    public int? ApprovalId_CurrentSequence { get; set; }
    public string? ApprovalId_ApprovalWorkflowId { get; set; }
    public string? ApprovalId_Payload { get; set; }
    public bool? ApprovalId_IsApprovalCompleted { get; set; }
    public string? ApprovalId_Comment { get; set; }
    public string? ApprovalId_EntityId { get; set; }
    public int? ApprovalId_TriedCount { get; set; }
    public bool? ApprovalId_IsActive { get; set; }
    public string? ApprovalId_CreatedByUserId { get; set; }
    public string? ApprovalId_UpdatedByUserId { get; set; }
    public string? ApprovalId_DeletedByUserId { get; set; }
    public bool? ApprovalId_IsDeleted { get; set; }
    public string? ApprovalId_Tags { get; set; }
    public string? ApprovalId_Caption { get; set; }
    public string? ApprovalId_ApprovalViewModelPayload { get; set; }
}