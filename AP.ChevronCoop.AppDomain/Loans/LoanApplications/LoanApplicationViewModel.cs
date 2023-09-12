namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;

public class LoanApplicationViewModel : BaseViewModel
{
  public string LoanProductId { get; set; }
  public string CustomerId { get; set; }
  public decimal Amount { get; set; }
  public decimal AdminFee { get; set; }
  public string RepaymentTenureUnit { get; set; }
  public int RepaymentPeriod { get; set; }
  public bool UseSpecialDeposit { get; set; }
  public string? SpecialDepositId { get; set; }
  public string? DestinationAccountId { get; set; }
  public string ApplicationNo { get; set; }
  public DateTimeOffset RepaymentCommencementDate { get; set; }
  public string AccountNo { get; set; } // Just a read only property
  public string ApprovalId { get; set; }
  public decimal Principal { get; set; }
  public string TenureUnit { get; set; }
  public decimal TenureValue { get; set; }
  public string CustomerDisbursementAccountId { get; set; }
  public string QualificationTargetProductId { get; set; }
  public string Status { get; set; }
  public string QualificationTargetProductType { get; set; }
}