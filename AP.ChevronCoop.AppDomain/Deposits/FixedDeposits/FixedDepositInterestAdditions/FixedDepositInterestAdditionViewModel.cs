namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestAdditions;
public partial class FixedDepositInterestAdditionViewModel : BaseViewModel
{

    public string FixedDepositAccountId { get; set; }

    public string InterestScheduleItemId { get; set; }
    public decimal InterestEarned { get; set; }

    public string TransactionJournalId { get; set; }
    public bool IsProcessed { get; set; }

    public DateTime ProcessedDate { get; set; }

}


