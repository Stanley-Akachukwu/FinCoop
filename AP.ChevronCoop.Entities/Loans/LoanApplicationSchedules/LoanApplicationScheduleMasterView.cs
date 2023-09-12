namespace AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;

public class LoanApplicationScheduleMasterView
{
  public long? RowNumber { get; set; }
  public string Id { get; set; }
  public string LoanApplicationId { get; set; }
  public int RepaymentNo { get; set; }
  public string TenureUnit { get; set; }
  public decimal TenureValue { get; set; }
  public DateTime? PeriodStartDate { get; set; }
  public DateTime? DueDate { get; set; }
  public int? DaysInPeriod { get; set; }
  public decimal PeriodPayment { get; set; }
  public decimal CumulativeTotal { get; set; }
  public decimal TotalBalance { get; set; }
  public decimal PeriodPrincipal { get; set; }
  public decimal CumulativePrincipal { get; set; }
  public decimal PrincipalBalance { get; set; }
  public decimal PeriodInterest { get; set; }
  public decimal CumulativeInterest { get; set; }
  public decimal InterestBalance { get; set; }
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
  public string? LoanApplicationId_ApplicationNo { get; set; }
  public string? LoanApplicationId_AccountNo { get; set; }
  public string? LoanApplicationId_LoanProductId { get; set; }
  public string? LoanApplicationId_ApprovalId { get; set; }
  public string? LoanApplicationId_CustomerId { get; set; }
  public decimal? LoanApplicationId_Principal { get; set; }
  public string? LoanApplicationId_TenureUnit { get; set; }
  public decimal? LoanApplicationId_TenureValue { get; set; }
  public DateTimeOffset? LoanApplicationId_RepaymentCommencementDate { get; set; }
  public bool? LoanApplicationId_UseSpecialDeposit { get; set; }
  public string? LoanApplicationId_SpecialDepositId { get; set; }
  public string? LoanApplicationId_CustomerDisbursementAccountId { get; set; }
  public string? LoanApplicationId_QualificationTargetProductId { get; set; }
  public string? LoanApplicationId_Status { get; set; }
  public string? LoanApplicationId_QualificationTargetProductType { get; set; }
  public bool? LoanApplicationId_IsActive { get; set; }
  public string? LoanApplicationId_CreatedByUserId { get; set; }
  public string? LoanApplicationId_UpdatedByUserId { get; set; }
  public string? LoanApplicationId_DeletedByUserId { get; set; }
  public bool? LoanApplicationId_IsDeleted { get; set; }
  public string? LoanApplicationId_Tags { get; set; }
  public string? LoanApplicationId_Caption { get; set; }
}