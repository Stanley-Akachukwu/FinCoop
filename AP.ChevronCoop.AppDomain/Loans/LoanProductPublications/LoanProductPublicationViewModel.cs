namespace AP.ChevronCoop.AppDomain.Loans.CustomerLoanProductPublications;

public class LoanProductPublicationViewModel : BaseViewModel
{
  public string PublicationType { get; set; }

  public string ProductId { get; set; }

  public List<string> EntityIds { get; set; }
}