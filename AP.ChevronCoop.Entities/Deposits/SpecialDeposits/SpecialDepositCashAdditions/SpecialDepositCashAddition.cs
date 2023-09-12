using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public class SpecialDepositCashAddition : BaseEntity<string>
    {
        public SpecialDepositCashAddition()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string SpecialDepositAccountId { get; set; }
        public SpecialDepositAccount SpecialDepositAccount { get; set; }
        public decimal Amount { get; set; }

        public string? CustomerPaymentDocumentId { get; set; }

        public CustomerPaymentDocument? CustomerPaymentDocument { get; set; }

        public DepositFundingSourceType ModeOfPayment { get; set; }
        public string BatchRefNo { get; set; }
        public string? TransactionJournalId { get; set; }
        public virtual TransactionJournal TransactionJournal { get; set; }
        public string ApprovalId { get; set; } 
        public Approval Approval { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;

        public override string DisplayCaption { get; }

        public override string DropdownCaption { get; }

        public override string ShortCaption { get; }


    }
}
