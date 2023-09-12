using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestAdditions
{
    public partial class SpecialDepositInterestAdditionViewModel : BaseViewModel
    {
        public string SpecialDepositAccountId { get; set; }
        public string InterestScheduleItemId { get; set; }
        public decimal InterestEarned { get; set; }
        public string TransactionJournalId { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime ProcessedDate { get; set; }

    }

}

