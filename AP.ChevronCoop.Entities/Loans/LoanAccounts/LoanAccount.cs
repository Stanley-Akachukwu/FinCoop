using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Loans.LoanAccounts;

public class LoanAccount : BaseEntity<string>
{
    public LoanAccount()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string AccountNo { get; set; }
    public string LoanApplicationId { get; set; }
    public virtual LoanApplication LoanApplication { get; set; }

    public string CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public string PrincipalBalanceAccountId { get; set; } //10,000 DR 1,000
    public virtual LedgerAccount PrincipalBalanceAccount { get; set; }

    public string PrincipalLossAccountId { get; set; }
    public virtual LedgerAccount PrincipalLossAccount { get; set; }

    public string EarnedInterestAccountId { get; set; } //1,000
    public virtual LedgerAccount EarnedInterestAccount { get; set; }

    //public string InterestEarnedAccountId { get; set; }
    //public string EarnedInterestAccountId { get; set; }
    //public virtual LedgerAccount InterestEarnedAccount { get; set; }
    //public virtual LedgerAccount EarnedInterestAccount { get; set; }

    public string InterestPayoutAccountId { get; set; }
    public virtual LedgerAccount InterestPayoutAccount { get; set; }
    public string InterestBalanceAccountId { get; set; } //1,000
    public virtual LedgerAccount InterestBalanceAccount { get; set; }
    
    public string UnearnedInterestAccountId { get; set; }
    public virtual LedgerAccount UnearnedInterestAccount { get; set; }

    public string InterestLossAccountId { get; set; }
    public virtual LedgerAccount InterestLossAccount { get; set; }
    
    public string InterestWaivedAccountId { get; set; }
    public virtual LedgerAccount InterestWaivedAccount { get; set; }

    public string ChargesAccruedAccountId { get; set; }
    public virtual LedgerAccount ChargesAccruedAccount { get; set; }

    public string ChargesIncomeAccountId { get; set; }
    public virtual LedgerAccount ChargesIncomeAccount { get; set; }

    public string ChargesWaivedAccountId { get; set; }
    public virtual LedgerAccount ChargesWaivedAccount { get; set; }
    public decimal Principal { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public DateTimeOffset RepaymentCommencementDate { get; set; }
    public bool UseSpecialDeposit { get; set; }
    public string? SpecialDepositAccountId { get; set; }
    public virtual SpecialDepositAccount? SpecialDepositAccount { get; set; }
    public string? DestinationAccountId { get; set; }
    public virtual CustomerBankAccount? DestinationAccount { get; set; }

    public virtual List<LoanDisbursement> Disbursements { get; set; }

    public bool IsClosed { get; set; } = false;
    public DateTime? DateClosed { get; set; }
    public string? ClosedByUserId { get; set; }
    public ApplicationUser? ClosedByUser { get; set; }
    //public string? LoanOffsetId { get; set; }
    //public virtual LoanOffset? LoanOffset { get; set; }
    
    public string? LoanTopupId { get; set; }
    public virtual LoanTopup? LoanTopup { get; set; }

    public string? RootParentAccountId { get; set; }
    public LoanAccount? RootParentAccount { get; set; }

    public string? ParentAccountId { get; set; }
    public LoanAccount? ParentAccount { get; set; }

    public LoanCreationType LoanCreationType { get; set; }

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}