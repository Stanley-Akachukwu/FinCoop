using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class GetLoanApplicationGuarantorsViewModel : MemberProfileViewModel
{
  public string LoanApplicationId { get; set; }
  public string GuarantorType { get; set; }
  public string GuarantorProfileId { get; set; }
}