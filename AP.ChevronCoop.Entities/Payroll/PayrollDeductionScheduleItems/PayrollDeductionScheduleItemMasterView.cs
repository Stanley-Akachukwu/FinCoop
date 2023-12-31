namespace AP.ChevronCoop.Entities.Payroll.PayrollDeductionScheduleItems;

public class PayrollDeductionScheduleItemMasterView
{
  public long? RowNumber { get; set; }
  public string Id { get; set; }
  public string? PayrollDeductionScheduleId { get; set; }
  public string BatchRefNo { get; set; }
  public string MemberId { get; set; }
  public string MemberName { get; set; }
  public string AccountNo { get; set; }
  public decimal Amount { get; set; }
  public string PayrollCode { get; set; }
  public string Narration { get; set; }
  public DateTime PayrollDate { get; set; }
  public DateTime AccountDueDate { get; set; }
  public string CurrentStatus { get; set; }
  public string DeductionType { get; set; }
  public string? LoanRepaymentScheduleId { get; set; }
  public string? SavingsAccountDeductionScheduleId { get; set; }
  public string? PayrollCronJobConfigId { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId { get; set; }
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
  public string? PayrollDeductionScheduleId_ScheduleName { get; set; }
  public string? PayrollDeductionScheduleId_ScheduleType { get; set; }
  public string? PayrollDeductionScheduleId_BankAccountId { get; set; }
  public string? PayrollDeductionScheduleId_SpecialDepositBankAccountId { get; set; }
  public string? PayrollDeductionScheduleId_FixedDepositBankAccountId { get; set; }
  public int? PayrollDeductionScheduleId_DeductionsCount { get; set; }
  public decimal? PayrollDeductionScheduleId_TotalDeductions { get; set; }
  public int? PayrollDeductionScheduleId_MinDecimalPlace { get; set; }
  public int? PayrollDeductionScheduleId_MaxDecimalPlace { get; set; }
  public DateTime? PayrollDeductionScheduleId_AdviseDate { get; set; }
  public DateTime? PayrollDeductionScheduleId_ExpectedDate { get; set; }
  public bool? PayrollDeductionScheduleId_IsPosted { get; set; }
  public DateTime? PayrollDeductionScheduleId_PayrollDate { get; set; }
  public bool? PayrollDeductionScheduleId_IsUploaded { get; set; }
  public DateTime? PayrollDeductionScheduleId_LastUploadedDate { get; set; }
  public bool? PayrollDeductionScheduleId_IsProcessed { get; set; }
  public DateTime? PayrollDeductionScheduleId_ProcessedDate { get; set; }
  public int? PayrollDeductionScheduleId_GenerateDeductionCronJobStatus { get; set; }
  public DateTime? PayrollDeductionScheduleId_GenerateDeductionCronJobStartedDate { get; set; }
  public DateTime? PayrollDeductionScheduleId_GenerateDeductionCronJobCompletedDate { get; set; }
  public int? PayrollDeductionScheduleId_ProcessDeductionCronJobStatus { get; set; }
  public DateTime? PayrollDeductionScheduleId_ProcessDeductionCronJobStartedDate { get; set; }
  public DateTime? PayrollDeductionScheduleId_ProcessDeductionCronJobCompletedDate { get; set; }
  public bool? PayrollDeductionScheduleId_IsActive { get; set; }
  public string? PayrollDeductionScheduleId_CreatedByUserId { get; set; }
  public string? PayrollDeductionScheduleId_UpdatedByUserId { get; set; }
  public string? PayrollDeductionScheduleId_DeletedByUserId { get; set; }
  public bool? PayrollDeductionScheduleId_IsDeleted { get; set; }
  public string? PayrollDeductionScheduleId_Tags { get; set; }
  public string? PayrollDeductionScheduleId_Caption { get; set; }
  public string? LoanRepaymentScheduleId_LoanAccountId { get; set; }
  public int? LoanRepaymentScheduleId_RepaymentNo { get; set; }
  public string? LoanRepaymentScheduleId_BatchRefNo { get; set; }
  public string? LoanRepaymentScheduleId_TenureUnit { get; set; }
  public decimal? LoanRepaymentScheduleId_TenureValue { get; set; }
  public DateTime? LoanRepaymentScheduleId_PeriodStartDate { get; set; }
  public DateTime? LoanRepaymentScheduleId_DueDate { get; set; }
  public int? LoanRepaymentScheduleId_DaysInPeriod { get; set; }
  public decimal? LoanRepaymentScheduleId_PeriodPayment { get; set; }
  public decimal? LoanRepaymentScheduleId_CumulativeTotal { get; set; }
  public decimal? LoanRepaymentScheduleId_TotalBalance { get; set; }
  public decimal? LoanRepaymentScheduleId_PeriodPrincipal { get; set; }
  public decimal? LoanRepaymentScheduleId_CumulativePrincipal { get; set; }
  public decimal? LoanRepaymentScheduleId_PrincipalBalance { get; set; }
  public decimal? LoanRepaymentScheduleId_PeriodInterest { get; set; }
  public decimal? LoanRepaymentScheduleId_CumulativeInterest { get; set; }
  public decimal? LoanRepaymentScheduleId_InterestBalance { get; set; }
  public bool? LoanRepaymentScheduleId_IsPaid { get; set; }
  public bool? LoanRepaymentScheduleId_IsPrincipalAllocated { get; set; }
  public bool? LoanRepaymentScheduleId_IsInterestAllocated { get; set; }
  public bool? LoanRepaymentScheduleId_IsProcessed { get; set; }
  public DateTime? LoanRepaymentScheduleId_ProcessedDate { get; set; }
  public bool? LoanRepaymentScheduleId_IsActive { get; set; }
  public string? LoanRepaymentScheduleId_CreatedByUserId { get; set; }
  public string? LoanRepaymentScheduleId_UpdatedByUserId { get; set; }
  public string? LoanRepaymentScheduleId_DeletedByUserId { get; set; }
  public bool? LoanRepaymentScheduleId_IsDeleted { get; set; }
  public string? LoanRepaymentScheduleId_Tags { get; set; }
  public string? LoanRepaymentScheduleId_Caption { get; set; }
  public decimal? LoanRepaymentScheduleId_ActualInterestAllocated { get; set; }
  public decimal? LoanRepaymentScheduleId_ActualInterestBalance { get; set; }
  public decimal? LoanRepaymentScheduleId_ActualPrincipalAllocated { get; set; }
  public decimal? LoanRepaymentScheduleId_ActualPrincipalBalance { get; set; }
  public string? SavingsAccountDeductionScheduleId_SavingsAccountId { get; set; }
  public string? SavingsAccountDeductionScheduleId_BatchRefNo { get; set; }
  public string? SavingsAccountDeductionScheduleId_MemberId { get; set; }
  public string? SavingsAccountDeductionScheduleId_EmployeeNo { get; set; }
  public string? SavingsAccountDeductionScheduleId_MemberName { get; set; }
  public string? SavingsAccountDeductionScheduleId_AccountNo { get; set; }
  public decimal? SavingsAccountDeductionScheduleId_Amount { get; set; }
  public string? SavingsAccountDeductionScheduleId_PayrollCode { get; set; }
  public string? SavingsAccountDeductionScheduleId_Narration { get; set; }
  public DateTime? SavingsAccountDeductionScheduleId_DueDate { get; set; }
  public string? SavingsAccountDeductionScheduleId_CurrentStatus { get; set; }
  public bool? SavingsAccountDeductionScheduleId_IsActive { get; set; }
  public string? SavingsAccountDeductionScheduleId_CreatedByUserId { get; set; }
  public string? SavingsAccountDeductionScheduleId_UpdatedByUserId { get; set; }
  public string? SavingsAccountDeductionScheduleId_DeletedByUserId { get; set; }
  public bool? SavingsAccountDeductionScheduleId_IsDeleted { get; set; }
  public string? SavingsAccountDeductionScheduleId_Tags { get; set; }
  public string? SavingsAccountDeductionScheduleId_Caption { get; set; }
  public string? PayrollCronJobConfigId_DeductionScheduleId { get; set; }
  public string? PayrollCronJobConfigId_CronJobType { get; set; }
  public string? PayrollCronJobConfigId_JobName { get; set; }
  public DateTime? PayrollCronJobConfigId_JobDate { get; set; }
  public string? PayrollCronJobConfigId_JobStatus { get; set; }
  public DateTime? PayrollCronJobConfigId_ComputationStartDate { get; set; }
  public DateTime? PayrollCronJobConfigId_ComputationEndDate { get; set; }
  public int? PayrollCronJobConfigId_RecordsProcessed { get; set; }
  public bool? PayrollCronJobConfigId_IsActive { get; set; }
  public string? PayrollCronJobConfigId_CreatedByUserId { get; set; }
  public string? PayrollCronJobConfigId_UpdatedByUserId { get; set; }
  public string? PayrollCronJobConfigId_DeletedByUserId { get; set; }
  public bool? PayrollCronJobConfigId_IsDeleted { get; set; }
  public string? PayrollCronJobConfigId_Tags { get; set; }
  public string? PayrollCronJobConfigId_Caption { get; set; }
  public decimal? PayrollCronJobConfigId_TotalAmount { get; set; }
  public long? PayrollCronJobConfigId_TotalCount { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_SpecialDepositAccountId { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_BatchRefNo { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_MemberId { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_EmployeeNo { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_MemberName { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_AccountNo { get; set; }
  public decimal? SpecialDepositAccountDeductionScheduleId_Amount { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_PayrollCode { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_Narration { get; set; }
  public DateTime? SpecialDepositAccountDeductionScheduleId_DueDate { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_CurrentStatus { get; set; }
  public bool? SpecialDepositAccountDeductionScheduleId_IsActive { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_CreatedByUserId { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_UpdatedByUserId { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_DeletedByUserId { get; set; }
  public bool? SpecialDepositAccountDeductionScheduleId_IsDeleted { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_Tags { get; set; }
  public string? SpecialDepositAccountDeductionScheduleId_Caption { get; set; }
}