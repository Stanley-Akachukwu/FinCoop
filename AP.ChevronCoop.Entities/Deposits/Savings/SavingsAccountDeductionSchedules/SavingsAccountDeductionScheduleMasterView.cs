namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;

public class SavingsAccountDeductionScheduleMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string SavingsAccountId { get; set; }
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
    public string? SavingsAccountId_ApplicationId { get; set; }
    public string? SavingsAccountId_AccountNo { get; set; }
    public string? SavingsAccountId_CustomerId { get; set; }
    public string? SavingsAccountId_DepositProductId { get; set; }
    public string? SavingsAccountId_LedgerDepositAccountId { get; set; }
    public string? SavingsAccountId_ChargesPayableAccountId { get; set; }
    public string? SavingsAccountId_ChargesAccruedAccountId { get; set; }
    public string? SavingsAccountId_ChargesWaivedAccountId { get; set; }
    public string? SavingsAccountId_ChargesIncomeAccountId { get; set; }
    public decimal? SavingsAccountId_PayrollAmount { get; set; }
    public bool? SavingsAccountId_IsClosed { get; set; }
    public DateTime? SavingsAccountId_DateClosed { get; set; }
    public string? SavingsAccountId_ClosedByUserId { get; set; }
    public decimal? SavingsAccountId_MaximumBalanceLimit { get; set; }
    public decimal? SavingsAccountId_MinimumBalanceLimit { get; set; }
    public decimal? SavingsAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? SavingsAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? SavingsAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? SavingsAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? SavingsAccountId_IsActive { get; set; }
    public string? SavingsAccountId_CreatedByUserId { get; set; }
    public string? SavingsAccountId_UpdatedByUserId { get; set; }
    public string? SavingsAccountId_DeletedByUserId { get; set; }
    public bool? SavingsAccountId_IsDeleted { get; set; }
    public string? SavingsAccountId_Tags { get; set; }
    public string? SavingsAccountId_Caption { get; set; }
}