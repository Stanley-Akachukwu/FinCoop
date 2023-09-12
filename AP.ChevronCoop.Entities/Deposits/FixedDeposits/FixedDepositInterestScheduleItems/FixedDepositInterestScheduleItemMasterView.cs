using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(FixedDepositInterestScheduleItemMasterView), Schema = "Deposits")]
public class FixedDepositInterestScheduleItemMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string? FixedDepositAccountId { get; set; }
    public string? FixedDepositInterestScheduleId { get; set; }
    public decimal OldBalance { get; set; }
    public decimal PeriodCashAddition { get; set; }
    public decimal InterestRate { get; set; }
    public decimal InterestEarned { get; set; }
    public decimal NewBalance { get; set; }
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
    public string? FixedDepositInterestScheduleId_CronJobConfigId { get; set; }
    public string? FixedDepositInterestScheduleId_ScheduleName { get; set; }
    public DateTime? FixedDepositInterestScheduleId_StartDate { get; set; }
    public DateTime? FixedDepositInterestScheduleId_EndDate { get; set; }
    public bool? FixedDepositInterestScheduleId_IsProcessed { get; set; }
    public DateTime? FixedDepositInterestScheduleId_ProcessedDate { get; set; }
    public bool? FixedDepositInterestScheduleId_IsActive { get; set; }
    public string? FixedDepositInterestScheduleId_CreatedByUserId { get; set; }
    public string? FixedDepositInterestScheduleId_UpdatedByUserId { get; set; }
    public string? FixedDepositInterestScheduleId_DeletedByUserId { get; set; }
    public bool? FixedDepositInterestScheduleId_IsDeleted { get; set; }
    public string? FixedDepositInterestScheduleId_Tags { get; set; }
    public string? FixedDepositInterestScheduleId_Caption { get; set; }
}