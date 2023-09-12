using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public class SpecialDepositWithdrawal : BaseEntity<string>
    {
        public SpecialDepositWithdrawal()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        
        public string SpecialDepositSourceAccountId { get; set; }
        public SpecialDepositAccount SpecialDepositSourceAccount { get; set; }

        public decimal Amount { get; set; }

        public WithdrawalAccountType WithdrawalDestinationType { get; set; }

        public string? CustomerDestinationBankAccountId { get; set; }
        public CustomerBankAccount? CustomerDestinationBankAccount { get; set; }


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
