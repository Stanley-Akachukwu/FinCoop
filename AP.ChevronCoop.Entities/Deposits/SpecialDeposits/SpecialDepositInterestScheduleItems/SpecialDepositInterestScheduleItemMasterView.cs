using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;

public class SpecialDepositInterestScheduleItemMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string? SpecialDepositAccountId { get; set; }
    public string? SpecialDepositInterestScheduleId { get; set; }
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
    public string? SpecialDepositInterestScheduleId_CronJobConfigId { get; set; }
    public string? SpecialDepositInterestScheduleId_ScheduleName { get; set; }
    public DateTime? SpecialDepositInterestScheduleId_StartDate { get; set; }
    public DateTime? SpecialDepositInterestScheduleId_EndDate { get; set; }
    public bool? SpecialDepositInterestScheduleId_IsProcessed { get; set; }
    public DateTime? SpecialDepositInterestScheduleId_ProcessedDate { get; set; }
    public bool? SpecialDepositInterestScheduleId_IsActive { get; set; }
    public string? SpecialDepositInterestScheduleId_CreatedByUserId { get; set; }
    public string? SpecialDepositInterestScheduleId_UpdatedByUserId { get; set; }
    public string? SpecialDepositInterestScheduleId_DeletedByUserId { get; set; }
    public bool? SpecialDepositInterestScheduleId_IsDeleted { get; set; }
    public string? SpecialDepositInterestScheduleId_Tags { get; set; }
    public string? SpecialDepositInterestScheduleId_Caption { get; set; }
}