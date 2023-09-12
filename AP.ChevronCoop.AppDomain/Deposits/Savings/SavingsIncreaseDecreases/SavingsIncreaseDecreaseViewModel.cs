using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
public partial class SavingsIncreaseDecreaseViewModel : BaseViewModel
{
    
    public string SavingsAccountId { get; set; }

    public decimal Amount { get; set; }
  
    public string ContributionChangeRequest { get; set; }

}


