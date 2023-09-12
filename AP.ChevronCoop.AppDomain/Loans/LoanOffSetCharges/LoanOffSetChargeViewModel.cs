namespace AP.ChevronCoop.AppDomain.Loans.LoanOffsetCharges;

public class LoanOffSetChargeViewModel : BaseViewModel
{
  public string LoanOffsetId { get; set; }
  public string OffsetChargeId { get; set; }
  public decimal TotalCharge { get; set; }
}