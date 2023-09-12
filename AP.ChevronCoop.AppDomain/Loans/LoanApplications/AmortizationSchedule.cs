using AP.ChevronCoop.Entities;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;



public class AmortizationSchedule
{

    public int RepaymentNo { get; set; }

    public string AccountNo { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public LoanProductType LoanProductType { get; set; }
    public string CustomerId { get; set; }
    public string CurrencyId { get; set; }

    public decimal Principal { get; set; }
    public decimal DaysInYear { get; set; } //360,365,366
    public decimal DaysInPeriod { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public Tenure RepaymentPeriod { get; set; } = Tenure.MONTHLY;

    public decimal InterestRate { get; set; }
    public InterestMethod InterestMethod { get; set; } = InterestMethod.SIMPLE;
    public InterestCalculationMethod InterestCalculationMethod { get; set; } = InterestCalculationMethod.FLAT_RATE;

    public DateTime PeriodStartDate { get; set; }
    public DateTime DueDate { get; set; }

    public decimal PeriodPayment { get; set; }
    public decimal CumulativeTotal { get; set; }
    public decimal TotalBalance { get; set; }

    public decimal PeriodPrincipal { get; set; }
    public decimal CumulativePrincipal { get; set; }
    public decimal PrincipalBalance { get; set; }

    public decimal PeriodInterest { get; set; }
    public decimal CumulativeInterest { get; set; }
    public decimal InterestBalance { get; set; }
}

