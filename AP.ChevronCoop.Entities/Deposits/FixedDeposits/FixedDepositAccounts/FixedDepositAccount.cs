using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;

public class FixedDepositAccount : BaseEntity<string>
{
    public FixedDepositAccount()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string ApplicationId { get; set; }
    public FixedDepositAccountApplication Application { get; set; }
    public string AccountNo { get; set; } //To be mapped to ledger acc code
    //public string ApplicationNo { get; set; }

    public string CustomerId { get; set; }
    public Customer Customer { get; set; }
    public string DepositProductId { get; set; }
    public DepositProduct DepositProduct { get; set; }
    public string DepositAccountId { get; set; }
    public virtual LedgerAccount DepositAccount { get; set; }
    public string ChargesAccruedAccountId { get; set; }
    public virtual LedgerAccount ChargesAccruedAccount { get; set; }

    public string ChargesIncomeAccountId { get; set; }
    public virtual LedgerAccount ChargesIncomeAccount { get; set; }

    public string InterestEarnedAccountId { get; set; }
    public virtual LedgerAccount InterestEarnedAccount { get; set; }
    public string InterestPayoutAccountId { get; set; }

    public virtual LedgerAccount InterestPayoutAccount { get; set; }


    public string ChargesWaivedAccountId { get; set; }
    public virtual LedgerAccount ChargesWaivedAccount { get; set; }

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

    public DateTime? LastInterestComputationDate { get; set; }

    public bool HasMature { get; set; } = false;
   
    public bool IsClosed { get; set; } = false;
    public DateTime? DateClosed { get; set; }
    public string? ClosedByUserId { get; set; }
    public ApplicationUser? ClosedByUser { get; set; }

    public List<FixedDepositLiquidation> FixedDepositLiquidations { get; set; } = new List<FixedDepositLiquidation>();


    public string? RootParentAccountId { get; set; }
    public FixedDepositAccount? RootParentAccount { get; set; }

    public string? ParentAccountId { get; set; }
    public FixedDepositAccount?  ParentAccount { get; set; }

    public decimal MaximumBalanceLimit { get; set; }
    public decimal MinimumBalanceLimit { get; set; }
    public decimal SingleWithdrawalLimit { get; set; }
    public decimal DailyWithdrawalLimit { get; set; }
    public decimal WeeklyWithdrawalLimit { get; set; }
    public decimal MonthlyWithdrawalLimit { get; set; }

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}