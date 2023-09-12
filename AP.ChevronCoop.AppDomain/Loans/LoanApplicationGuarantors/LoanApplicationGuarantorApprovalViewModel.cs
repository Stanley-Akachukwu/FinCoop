namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class LoanApplicationGuarantorApprovalViewModel : BaseViewModel
{
  public string LoanApplicationId { get; set; }
  public string GuarantorId { get; set; }
  public string GuarantorType { get; set; }
  public decimal Amount { get; set; }
}