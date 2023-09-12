using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplications;

namespace AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;

public class LoanApplicationSchedule : BaseEntity<string>
{
    public LoanApplicationSchedule()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string LoanApplicationId { get; set; }
    public virtual LoanApplication LoanApplication { get; set; }

    public int RepaymentNo { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }


    public DateTime? PeriodStartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public int? DaysInPeriod { get; set; }

    public decimal PeriodPayment { get; set; }
    public decimal CumulativeTotal { get; set; }
    public decimal TotalBalance { get; set; }

    public decimal PeriodPrincipal { get; set; }
    public decimal CumulativePrincipal { get; set; }
    public decimal PrincipalBalance { get; set; }

    public decimal PeriodInterest { get; set; }
    public decimal CumulativeInterest { get; set; }
    public decimal InterestBalance { get; set; }

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}