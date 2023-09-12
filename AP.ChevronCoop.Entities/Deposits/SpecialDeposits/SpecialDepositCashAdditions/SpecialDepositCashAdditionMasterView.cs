namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;

public class SpecialDepositCashAdditionMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string? SpecialDepositAccountId { get; set; }
    public decimal Amount { get; set; }
    public string? CustomerPaymentDocumentId { get; set; }
    public string ModeOfPayment { get; set; }
    public string? BatchRefNo { get; set; }
    public string? TransactionJournalId { get; set; }
    public string? ApprovalId { get; set; }
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
    public string? SpecialDepositAccountId_ApplicationId { get; set; }
    public string? SpecialDepositAccountId_AccountNo { get; set; }
    public string? SpecialDepositAccountId_CustomerId { get; set; }
    public string? SpecialDepositAccountId_DepositProductId { get; set; }
    public string? SpecialDepositAccountId_DepositAccountId { get; set; }
    public string? SpecialDepositAccountId_ChargesAccruedAccountId { get; set; }
    public string? SpecialDepositAccountId_ChargesIncomeAccountId { get; set; }
    public string? SpecialDepositAccountId_ChargesWaivedAccountId { get; set; }
    public string? SpecialDepositAccountId_InterestEarnedAccountId { get; set; }
    public string? SpecialDepositAccountId_InterestPayoutAccountId { get; set; }
    public decimal? SpecialDepositAccountId_FundingAmount { get; set; }
    public decimal? SpecialDepositAccountId_InterestRate { get; set; }
    public DateTime? SpecialDepositAccountId_LastInterestComputationDate { get; set; }
    public decimal? SpecialDepositAccountId_MaximumBalanceLimit { get; set; }
    public decimal? SpecialDepositAccountId_MinimumBalanceLimit { get; set; }
    public decimal? SpecialDepositAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? SpecialDepositAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? SpecialDepositAccountId_IsClosed { get; set; }
    public DateTime? SpecialDepositAccountId_DateClosed { get; set; }
    public string? SpecialDepositAccountId_ClosedByUserId { get; set; }
    public bool? SpecialDepositAccountId_IsActive { get; set; }
    public string? SpecialDepositAccountId_CreatedByUserId { get; set; }
    public string? SpecialDepositAccountId_UpdatedByUserId { get; set; }
    public string? SpecialDepositAccountId_DeletedByUserId { get; set; }
    public bool? SpecialDepositAccountId_IsDeleted { get; set; }
    public string? SpecialDepositAccountId_Tags { get; set; }
    public string? SpecialDepositAccountId_Caption { get; set; }
    public string? CustomerPaymentDocumentId_CustomerId { get; set; }
    public string? CustomerPaymentDocumentId_DocumentData { get; set; }
    public string? CustomerPaymentDocumentId_MimeType { get; set; }
    public string? CustomerPaymentDocumentId_FileName { get; set; }
    public int? CustomerPaymentDocumentId_FileSize { get; set; }
    public string? CustomerPaymentDocumentId_DocumentType { get; set; }
    public bool? CustomerPaymentDocumentId_IsActive { get; set; }
    public string? CustomerPaymentDocumentId_CreatedByUserId { get; set; }
    public string? CustomerPaymentDocumentId_UpdatedByUserId { get; set; }
    public string? CustomerPaymentDocumentId_DeletedByUserId { get; set; }
    public bool? CustomerPaymentDocumentId_IsDeleted { get; set; }
    public string? CustomerPaymentDocumentId_Tags { get; set; }
    public string? CustomerPaymentDocumentId_Caption { get; set; }
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