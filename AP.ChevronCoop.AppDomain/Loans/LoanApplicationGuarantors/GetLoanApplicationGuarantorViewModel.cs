using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Entities;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class GetLoanApplicationGuarantorViewModel
{
  public LoanApplicationViewModel LoanApplication { get; set; }
  public CustomerViewModel Guarantor { get; set; }
  public CustomerViewModel Applicant { get; set; }
  public LoanProductViewModel Product { get; set; }
  
  public ApprovalStatus Status { get; set; }
  public DateTime? ApprovedOn { get; set; }
  public string Comment { get; set; }
  
}