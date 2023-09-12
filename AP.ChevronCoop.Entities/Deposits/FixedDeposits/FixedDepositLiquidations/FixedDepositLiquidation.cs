using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositLiquidationCharges;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations
{

    public class FixedDepositLiquidation : BaseEntity<string>
    {
        public FixedDepositLiquidation()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string FixedDepositAccountId { get; set; }
        public FixedDepositAccount FixedDepositAccount { get; set; }

        public WithdrawalAccountType? LiquidationAccountType { get; set; }
        public string? SavingsLiquidationAccountId { get; set; }
        public SavingsAccount? SavingsLiquidationAccount { get; set; }
        public string? SpecialDepositLiquidationAccountId { get; set; }
        public SpecialDepositAccount? SpecialDepositLiquidationAccount { get; set; }
        public string? CustomerBankLiquidationAccountId { get; set; }
        public CustomerBankAccount? CustomerBankLiquidationAccount { get; set; }

        public string? TransactionJournalId { get; set; }
        public virtual TransactionJournal TransactionJournal { get; set; }

        public List<FixedDepositLiquidationCharge> LiquidationCharges { get; set; }

        public string? ApprovalId { get; set; }
        public Approval Approval { get; set; }

        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }

        public DateTime? LiquidationDate { get; set; }
        public DateTime? MaturityDate { get; set; }

        public bool IsMatured { get; set; } = false;

        public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;

        public override string DisplayCaption { get; }

        public override string DropdownCaption { get; }

        public override string ShortCaption { get; }
    }
}
