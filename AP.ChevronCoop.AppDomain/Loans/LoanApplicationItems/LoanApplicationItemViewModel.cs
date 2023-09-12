namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;

public class LoanApplicationItemViewModel : BaseViewModel
{
  public string LoanApplicationId { get; set; }
  public string ItemType { get; set; }
  public string Name { get; set; }
  public string BrandName { get; set; }
  public string Model { get; set; }
  public string Color { get; set; }
  public decimal Amount { get; set; }
}