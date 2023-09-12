using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public partial class SpecialDepositCashAdditionViewModel : BaseViewModel
    {
        public string SpecialDepositAccountId { get; set; }
        public decimal Amount { get; set; }
        public string CustomerPaymentDocumentId { get; set; }
        public string ModeOfPayment { get; set; }
        public string BatchRefNo { get; set; }
        public string TransactionJournalId { get; set; }
        public bool IsProcessed { get; set; }
        public string ApprovalId { get; set; }
        public DateTime? ProcessedDate { get; set; }

    }

}

