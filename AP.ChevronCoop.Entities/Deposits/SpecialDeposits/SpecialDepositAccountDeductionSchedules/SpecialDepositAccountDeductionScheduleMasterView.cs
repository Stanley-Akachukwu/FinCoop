using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;

public class SpecialDepositAccountDeductionScheduleMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string SpecialDepositAccountId { get; set; }
    public string? BatchRefNo { get; set; }
    public string? MemberId { get; set; }
    public string? EmployeeNo { get; set; }
    public string? MemberName { get; set; }
    public string? AccountNo { get; set; }
    public decimal Amount { get; set; }
    public string? PayrollCode { get; set; }
    public string? Narration { get; set; }
    public DateTime DueDate { get; set; }
    public string? CurrentStatus { get; set; }
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
}