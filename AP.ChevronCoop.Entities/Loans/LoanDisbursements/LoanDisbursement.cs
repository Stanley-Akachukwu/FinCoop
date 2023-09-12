using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Loans.LoanDisbursements;

// config mapping 
public class LoanDisbursement : BaseEntity<string>
{
    public LoanDisbursement()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
        LoanDisbursementCharges = new List<LoanDisbursementCharge>();
    }

    public string LoanAccountId { get; set; }
    public virtual LoanAccount LoanAccount { get; set; }
    public decimal Amount { get; set; }

    public string? ApprovedByUserId { get; set; }
    public virtual ApplicationUser? ApprovedByUser { get; set; }

    public string? DisbursedByUserId { get; set; }
    public virtual ApplicationUser? DisbursedByUser { get; set; }
    public DisbursementStatusType DisbursementStatus { get; set; }

    public string? DisbursementAccountId { get; set; }
    public virtual CompanyBankAccount? DisbursementAccount { get; set; }
    public DateTimeOffset? DisbursementDate { get; set; }

    public LoanDisbursementMode DisbursementMode { get; set; }
    public string? SpecialDepositAccountId { get; set; }
    public virtual SpecialDepositAccount? SpecialDepositAccount { get; set; }

    public string? CustomerBankAccountId { get; set; }
    public virtual CustomerBankAccount? CustomerBankAccount { get; set; }

    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal? TransactionJournal { get; set; }

    public string? ApprovalId { get; set; }
    public virtual Approval? Approval { get; set; }
    public DateTimeOffset? ApprovalDate { get; set; }


    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;
    public TransactionType? TransactionType { get; set; }

    // charges 
    public virtual List<LoanDisbursementCharge> LoanDisbursementCharges { get; set; }

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }



    public void HandleLoanDisbursement()
    {

    }
}


