using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications
{
    public class FixedDepositAccountApplication : BaseEntity<string>
    {
        public FixedDepositAccountApplication()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public string ApplicationNo { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string DepositProductId { get; set; }
        public DepositProduct DepositProduct { get; set; }
        
        //savings acc fields
        public decimal Amount { get; set; }
        //fixed deposit acc fields
        public Tenure TenureUnit { get; set; }
        public decimal TenureValue { get; set; }
        //special deposit/fixed deposit fields
        public decimal InterestRate { get; set; }

        public MaturityInstructionType MaturityInstructionType { get; set; }
        //Liquidation accounts
        public WithdrawalAccountType LiquidationAccountType { get; set; }
        public string? SavingsLiquidationAccountId { get; set; }
        public SavingsAccount? SavingsLiquidationAccount { get; set; }
        public string? SpecialDepositLiquidationAccountId { get; set; }
        public SpecialDepositAccount? SpecialDepositLiquidationAccount { get; set; }
        public string? CustomerBankLiquidationAccountId { get; set; }
        public CustomerBankAccount? CustomerBankLiquidationAccount { get; set; }
        
        //funding source
        public DepositFundingSourceType ModeOfPayment { get; set; }
        public string? SpecialDepositFundingSourceAccountId { get; set; }
        public SpecialDepositAccount? SpecialDepositFundingSourceAccount { get; set; }
        public string? CustomerBankFundingSourceAccountId { get; set; }
        public CustomerBankAccount? CustomerBankFundingSourceAccount { get; set; }
        public string? PaymentDocumentId { get; set; }
        public CustomerPaymentDocument? PaymentDocument { get; set; }
        
        
        // public ApprovalStatus Status { get; set; }
        
        public string? ApprovalId { get; set; }
        public Approval Approval { get; set; }
        
        //public string PaymentAccountNumber { get; set; } //To capture  Customer selected existing account number
        //public string PaymentBankName { get; set; }
        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }
}

