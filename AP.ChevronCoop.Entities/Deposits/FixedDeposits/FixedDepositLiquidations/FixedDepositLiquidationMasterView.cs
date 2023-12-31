namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;

public class FixedDepositLiquidationMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string FixedDepositAccountId { get; set; }
    public string? LiquidationAccountType { get; set; }
    public string? SavingsLiquidationAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId { get; set; }
    public string? CustomerBankLiquidationAccountId { get; set; }
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
    public bool IsMatured { get; set; }
    public DateTime? LiquidationDate { get; set; }
    public DateTime? MaturityDate { get; set; }
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
    public string? SavingsLiquidationAccountId_ApplicationId { get; set; }
    public string? SavingsLiquidationAccountId_AccountNo { get; set; }
    public string? SavingsLiquidationAccountId_CustomerId { get; set; }
    public string? SavingsLiquidationAccountId_DepositProductId { get; set; }
    public string? SavingsLiquidationAccountId_LedgerDepositAccountId { get; set; }
    public string? SavingsLiquidationAccountId_ChargesPayableAccountId { get; set; }
    public string? SavingsLiquidationAccountId_ChargesAccruedAccountId { get; set; }
    public string? SavingsLiquidationAccountId_ChargesWaivedAccountId { get; set; }
    public string? SavingsLiquidationAccountId_ChargesIncomeAccountId { get; set; }
    public decimal? SavingsLiquidationAccountId_PayrollAmount { get; set; }
    public bool? SavingsLiquidationAccountId_IsClosed { get; set; }
    public DateTime? SavingsLiquidationAccountId_DateClosed { get; set; }
    public string? SavingsLiquidationAccountId_ClosedByUserId { get; set; }
    public decimal? SavingsLiquidationAccountId_MaximumBalanceLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_MinimumBalanceLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? SavingsLiquidationAccountId_IsActive { get; set; }
    public string? SavingsLiquidationAccountId_CreatedByUserId { get; set; }
    public string? SavingsLiquidationAccountId_UpdatedByUserId { get; set; }
    public string? SavingsLiquidationAccountId_DeletedByUserId { get; set; }
    public bool? SavingsLiquidationAccountId_IsDeleted { get; set; }
    public string? SavingsLiquidationAccountId_Tags { get; set; }
    public string? SavingsLiquidationAccountId_Caption { get; set; }
    public string? SpecialDepositLiquidationAccountId_ApplicationId { get; set; }
    public string? SpecialDepositLiquidationAccountId_AccountNo { get; set; }
    public string? SpecialDepositLiquidationAccountId_CustomerId { get; set; }
    public string? SpecialDepositLiquidationAccountId_DepositProductId { get; set; }
    public string? SpecialDepositLiquidationAccountId_DepositAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_ChargesAccruedAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_ChargesIncomeAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_ChargesWaivedAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_InterestEarnedAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_InterestPayoutAccountId { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_FundingAmount { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_InterestRate { get; set; }
    public DateTime? SpecialDepositLiquidationAccountId_LastInterestComputationDate { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_MaximumBalanceLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_MinimumBalanceLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? SpecialDepositLiquidationAccountId_IsClosed { get; set; }
    public DateTime? SpecialDepositLiquidationAccountId_DateClosed { get; set; }
    public string? SpecialDepositLiquidationAccountId_ClosedByUserId { get; set; }
    public bool? SpecialDepositLiquidationAccountId_IsActive { get; set; }
    public string? SpecialDepositLiquidationAccountId_CreatedByUserId { get; set; }
    public string? SpecialDepositLiquidationAccountId_UpdatedByUserId { get; set; }
    public string? SpecialDepositLiquidationAccountId_DeletedByUserId { get; set; }
    public bool? SpecialDepositLiquidationAccountId_IsDeleted { get; set; }
    public string? SpecialDepositLiquidationAccountId_Tags { get; set; }
    public string? SpecialDepositLiquidationAccountId_Caption { get; set; }
    public string? CustomerBankLiquidationAccountId_LedgerAccountId { get; set; }
    public string? CustomerBankLiquidationAccountId_CustomerId { get; set; }
    public string? CustomerBankLiquidationAccountId_BankId { get; set; }
    public string? CustomerBankLiquidationAccountId_AccountName { get; set; }
    public string? CustomerBankLiquidationAccountId_AccountNumber { get; set; }
    public string? CustomerBankLiquidationAccountId_BVN { get; set; }
    public string? CustomerBankLiquidationAccountId_Branch { get; set; }
    public bool? CustomerBankLiquidationAccountId_IsActive { get; set; }
    public string? CustomerBankLiquidationAccountId_CreatedByUserId { get; set; }
    public string? CustomerBankLiquidationAccountId_UpdatedByUserId { get; set; }
    public string? CustomerBankLiquidationAccountId_DeletedByUserId { get; set; }
    public bool? CustomerBankLiquidationAccountId_IsDeleted { get; set; }
    public string? CustomerBankLiquidationAccountId_Tags { get; set; }
    public string? CustomerBankLiquidationAccountId_Caption { get; set; }
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