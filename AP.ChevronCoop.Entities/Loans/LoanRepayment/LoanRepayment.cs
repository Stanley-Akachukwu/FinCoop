using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Loans.LoanRepayment;

public class LoanRepayment : BaseEntity<string>
{
    public LoanRepayment()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public LoanRepaymentMode RepaymentMode { get; set; }


    public string LoanAccountId { get; set; }
    public virtual LoanAccount LoanAccount { get; set; }

    //mandatory repayment schedule allocation
    public string? LoanRepaymentScheduleId { get; set; }
    public LoanRepaymentSchedule? LoanRepaymentSchedule { get; set; }

    public string? PayrollDeductionScheduleItemId { get; set; }
    public PayrollDeductionScheduleItem? PayrollDeductionScheduleItem { get; set; }


    public string? LoanOffsetId { get; set; }
    public virtual LoanOffset? LoanOffset { get; set; }


    // public string? LoanTopupId { get; set; }
    // public virtual LoanTopup LoanTopup { get; set; }
    
    
    public string? ApprovalId { get; set; }
    public virtual Approval? Approval { get; set; }

    public decimal Amount { get; set; } //break down into principal and interest
    public decimal Principal { get; set; } 
    public decimal Interest { get; set; }

    public DateTimeOffset? PeriodStartDate { get; set; }
    public DateTimeOffset? RepaymentDate { get; set; }

    // public DateTimeOffset? ApprovalDate { get; set; }
    // public string ApprovedByUserId { get; set; }
    // public virtual ApplicationUser ApprovedByUser { get; set; }


    public string? PaymentAccountId { get; set; }
    public virtual CompanyBankAccount? PaymentAccount { get; set; }
    public string? CustomerBankAccountId { get; set; }
    public virtual CustomerBankAccount? CustomerBankAccount { get; set; }


    public string? TransactionJournalId { get; set; }
    public virtual TransactionJournal? TransactionJournal { get; set; }


    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;

    // charges 
    public virtual List<LoanRepaymentCharge> LoanRepaymentCharges { get; set; }
    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }



    public void HandleLoanDisbursement()
    {

    }
}


