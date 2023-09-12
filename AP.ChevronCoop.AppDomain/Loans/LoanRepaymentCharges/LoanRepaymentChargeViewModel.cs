using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepaymentCharges;

public class LoanRepaymentChargeViewModel : BaseViewModel
{
  public string LoanRepaymentId { get; set; }
  public string RepaymentChargeId { get; set; }
  public string ChargeType { get; set; }
  public decimal TotalCharge { get; set; }
}