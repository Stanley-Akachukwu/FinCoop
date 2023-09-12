using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer
{
    public class SpecialDepositFundTransfer : BaseEntity<string>
    {
        public SpecialDepositFundTransfer()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }


        public string SpecialDepositAccountId { get; set; }
        public SpecialDepositAccount SpecialDepositAccount { get; set; }

        public decimal Amount { get; set; }

        public DestinationAccountType DestinationAccountType { get; set; }

        public string? SavingsDestinationAccountId { get; set; } 
        public SavingsAccount? SavingsDestinationAccount { get; set; }
        
        public string? FixedDepositDestinationAccountId { get; set; }
        public FixedDepositAccount? FixedDepositDestinationAccount { get; set; }

        public string? TransactionJournalId { get; set; }
        public virtual TransactionJournal TransactionJournal { get; set; }

        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;
        public string ApprovalId { get; set; }
        public Approval Approval { get; set; }
        public override string DisplayCaption { get; }

        public override string DropdownCaption { get; }

        public override string ShortCaption { get; }


    }
}
