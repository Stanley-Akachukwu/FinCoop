using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;

namespace AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;



public class LoanRepaymentSchedule : BaseEntity<string>
{
    public LoanRepaymentSchedule()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string LoanAccountId { get; set; }
    public LoanAccount LoanAccount { get; set; }

    public int RepaymentNo { get; set; }
    public string BatchRefNo { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }


    public DateTime PeriodStartDate { get; set; }
    public DateTime DueDate { get; set; }
    public int? DaysInPeriod { get; set; }

    public decimal PeriodPayment { get; set; }
    public decimal CumulativeTotal { get; set; }
    public decimal TotalBalance { get; set; }


    public decimal PeriodPrincipal { get; set; }
    public decimal CumulativePrincipal { get; set; }
    public decimal PrincipalBalance { get; set; }
    public decimal ActualPrincipalAllocated { get; set; }
    public decimal ActualPrincipalBalance { get; set; }


    public decimal PeriodInterest { get; set; }
    public decimal CumulativeInterest { get; set; }
    public decimal InterestBalance { get; set; }
    public decimal ActualInterestAllocated { get; set; }
    public decimal ActualInterestBalance { get; set; }

    public bool IsPaid { get; set; } //expected amount is fully allocated and paid
    public bool IsPrincipalAllocated { get; set; }
    public bool IsInterestAllocated { get; set; }

    public bool IsProcessed { get; set; }
    public DateTime? ProcessedDate { get; set; }


    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}
