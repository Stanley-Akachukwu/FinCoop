using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursementCharges;

public class LoanDisbursementChargeViewModel : BaseViewModel
{
  public string LoanDisbursementId { get; set; }
  public string DisbursementChargeId { get; set; }
  public string ChargeType { get; set; }
  public decimal TotalCharge { get; set; }
}