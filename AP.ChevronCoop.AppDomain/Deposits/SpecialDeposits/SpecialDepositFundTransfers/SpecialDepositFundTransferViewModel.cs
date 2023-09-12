using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers
{
    public partial class SpecialDepositFundTransferViewModel : BaseViewModel
    {
        public string SpecialDepositAccountId { get; set; }
        public decimal Amount { get; set; }
        public string DestinationAccountType { get; set; }
        public string SavingAccountDestinationId { get; set; }
        public string FixedDepositDestinationAccountId { get; set; }
        public string TransactionJournalId { get; set; }
        public bool IsProcessed { get; set; }

        public DateTime? ProcessedDate { get; set; }

    }

}

