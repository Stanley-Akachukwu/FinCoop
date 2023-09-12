using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositChangeInMaturities
{
    public class FixedDepositChangeInMaturity : BaseEntity<string> // For change Instruction update log
    {
        public FixedDepositChangeInMaturity()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public MaturityInstructionType MaturityInstructionType { get; set; }
        public string FixedDepositAccountId { get; set; }
        public FixedDepositAccount FixedDepositAccount { get; set; }
        public WithdrawalAccountType LiquidationAccountType { get; set; }
        public string? SavingsLiquidationAccountId { get; set; }
        public SavingsAccount? SavingsLiquidationAccount { get; set; }
        public string? SpecialDepositLiquidationAccountId { get; set; }
        public SpecialDepositAccount? SpecialDepositLiquidationAccount { get; set; }
        public string? CustomerBankLiquidationAccountId { get; set; }
        public CustomerBankAccount? CustomerBankLiquidationAccount { get; set; }
        
        
        
        public string? ApprovalId { get; set; }
        public Approval Approval { get; set; }

        public override string DisplayCaption { get; }

        public override string DropdownCaption { get; }

        public override string ShortCaption { get; }
    }
}
