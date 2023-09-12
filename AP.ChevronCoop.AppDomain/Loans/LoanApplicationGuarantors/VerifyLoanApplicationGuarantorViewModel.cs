using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class VerifyLoanApplicationGuarantorViewModel : MemberProfileViewModel
{
  public string GuarantorType { get; set; }
  public string GuarantorCustomerId { get; set; }
  public string GuarantorMembershipId { get; set; }
  public int YearsOfService { get; set; }
  public decimal TotalRunningLoan { get; set; }

  public string FullName => $"{LastName} {MiddleName} {FirstName}";
}