using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;

public class GenerateScheduleCommand : IRequest<CommandResult<GenerateScheduleViewModel>>
{
    public string CustomerId { get; set; }
    public string LoanProductId { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public decimal DaysInYear { get; set; } //360,365,366

    public Tenure RepaymentPeriod { get; set; } = Tenure.MONTHLY;
    public Tenure CompoundingPeriod { get; set; } = Tenure.MONTHLY;

    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; }
    public InterestMethod InterestMethod { get; set; } = InterestMethod.SIMPLE;
    public InterestCalculationMethod InterestCalculationMethod { get; set; } = InterestCalculationMethod.FLAT_RATE;
    public DateTime CommencementDate { get; set; }
}