using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopupCharges;

public class LoanTopupChargeViewModel : BaseViewModel
{
  public string LoanTopupId { get; set; }
  public string TopupChargeId { get; set; }
  public string ChargeType { get; set; }
  public decimal TotalCharge { get; set; }
  public string LoanTopupId1 { get; set; }
}