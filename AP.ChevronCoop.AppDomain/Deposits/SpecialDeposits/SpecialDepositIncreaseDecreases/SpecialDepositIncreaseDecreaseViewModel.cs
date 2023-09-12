using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
public partial class SpecialDepositIncreaseDecreaseViewModel : BaseViewModel
{

    public string SpecialDepositAccountId { get; set; }

    public decimal Amount { get; set; }

    public string ContributionChangeRequest { get; set; }

}


