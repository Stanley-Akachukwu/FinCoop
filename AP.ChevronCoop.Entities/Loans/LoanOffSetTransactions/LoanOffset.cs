using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.LoanOffsetTransactions;

public class LoanOffset : BaseEntity<string>
{
    public LoanOffset()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string LoanAccountId { get; set; }
    public virtual LoanAccount LoanAccount { get; set; }

    public decimal OffsetAmount { get; set; }

    //snapshot fields for existing loan account
    public decimal OldPrincipalBalance { get; set; }
    public decimal NewPrincipalBalance { get; set; }
    public decimal OldInterestBalance { get; set; }
    public decimal NewInterestBalance { get; set; }

    //check and snapshot field
    public decimal TotalOffsetCharges { get; set; }
    // charges 
    public virtual List<LoanOffSetCharge> LoanOffSetCharges { get; set; }

    public bool IsLiquidated { get; set; }

    public AllowedOffsetType AllowedOffsetType { get; set; }
    public LoanRepaymentMode LoanRepaymentMode { get; set; }
    public DateTimeOffset OffSetRepaymentDate { get; set; }
    public string? SavingsAccountId { get; set; }
    public SavingsAccount? SavingsAccount { get; set; }
    public string? SpecialDepositAccountId { get; set; }
    public SpecialDepositAccount? SpecialDepositAccount { get; set; }
    public string? CustomerBankAccountId { get; set; }
    public CustomerBankAccount? CustomerBankAccount { get; set; }
    //funding source
    public DepositFundingSourceType ModeOfPayment { get; set; }
    public string? CustomerPaymentDocumentId { get; set; }
    public CustomerPaymentDocument? CustomerPaymentDocument { get; set; }

    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal? TransactionJournal { get; set; }
    
    public string? ApprovalId { get; set; }
    public Approval? Approval { get; set; }

    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;
    public List<string> RepaymentSchedules { get; set; }
    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}