namespace AP.ChevronCoop.AppDomain.Loans.LoanRepaymentSchedules;

public class LoanRepaymentScheduleViewModel : BaseViewModel
{
  public DateTime Date { get; set; }
  public decimal Principal { get; set; }
  public decimal Interest { get; set; }
  public decimal Balance { get; set; }
}