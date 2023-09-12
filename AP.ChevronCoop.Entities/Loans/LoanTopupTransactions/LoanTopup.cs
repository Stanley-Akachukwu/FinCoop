using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;

namespace AP.ChevronCoop.Entities.LoanTopupTransactions;

public class LoanTopup : BaseEntity<string>
{
    public LoanTopup()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }
    public string LoanAccountId { get; set; }
    public virtual LoanAccount LoanAccount { get; set; }
    
    public decimal TopupAmount { get; set; }
    
    public TopupFundingSourceType DestinationType { get; set; }
    
    public string? SpecialDepositAccountId { get; set; }
    public SpecialDepositAccount? SpecialDepositAccount { get; set; }
    
    public string? CustomerBankAccountId { get; set; }
    public CustomerBankAccount? CustomerBankAccount { get; set; }

    //snapshot fields for existing loan account
    public decimal OldPrincipalBalance { get; set; }
    public decimal NewPrincipalBalance { get; set; }
    public decimal OldInterestBalance { get; set; }
    public decimal NewInterestBalance { get; set; }
    
    //check and snapshot field
    public decimal TotalTopupCharges { get; set; }
    // charges 
    public virtual List<LoanTopupCharge> LoanTopupCharges { get; set; }

    public DateTimeOffset TopupDate { get; set; }
    public DateTimeOffset CommencementDate { get; set; }
    
    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal? TransactionJournal { get; set; }
    
    public string? ApprovalId { get; set; }
    public Approval? Approval { get; set; }


    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}