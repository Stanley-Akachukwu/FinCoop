using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;

[Table(nameof(FixedDepositLiquidationChargeMasterView), Schema = "Deposits")]
public class FixedDepositLiquidationChargeMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string? FixedDepositLiquidationId { get; set; }
    public string ChargeType { get; set; }
    public decimal LiquidationCharge { get; set; }
    public string? TransactionJournalId { get; set; }
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
    public string? FixedDepositLiquidationId_FixedDepositAccountId { get; set; }
    public string? FixedDepositLiquidationId_LiquidationAccountType { get; set; }
    public string? FixedDepositLiquidationId_SavingsLiquidationAccountId { get; set; }
    public string? FixedDepositLiquidationId_SpecialDepositLiquidationAccountId { get; set; }
    public string? FixedDepositLiquidationId_CustomerBankLiquidationAccountId { get; set; }
    public string? FixedDepositLiquidationId_TransactionJournalId { get; set; }
    public string? FixedDepositLiquidationId_ApprovalId { get; set; }
    public bool? FixedDepositLiquidationId_IsProcessed { get; set; }
    public DateTime? FixedDepositLiquidationId_ProcessedDate { get; set; }
    public string? FixedDepositLiquidationId_Status { get; set; }
    public bool? FixedDepositLiquidationId_IsActive { get; set; }
    public string? FixedDepositLiquidationId_CreatedByUserId { get; set; }
    public string? FixedDepositLiquidationId_UpdatedByUserId { get; set; }
    public string? FixedDepositLiquidationId_DeletedByUserId { get; set; }
    public bool? FixedDepositLiquidationId_IsDeleted { get; set; }
    public string? FixedDepositLiquidationId_Tags { get; set; }
    public string? FixedDepositLiquidationId_Caption { get; set; }
    public bool? FixedDepositLiquidationId_IsMatured { get; set; }
    public DateTime? FixedDepositLiquidationId_LiquidationDate { get; set; }
    public DateTime? FixedDepositLiquidationId_MaturityDate { get; set; }
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