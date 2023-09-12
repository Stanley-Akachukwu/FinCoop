using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;

public class CreateLoanApplicationScheduleCommand : IRequest<CommandResult<List<LoanApplicationScheduleViewModel>>>
{
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public DateTime CommencementDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Interest { get; set; }




    public string? LoanApplicationId { get; set; }

    public int? RepaymentNo { get; set; }


    public DateTime? PeriodStartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public int? DaysInPeriod { get; set; }

    public decimal? PeriodPayment { get; set; }
    public decimal? CumulativeTotal { get; set; }
    public decimal? TotalBalance { get; set; }

    public decimal? PeriodPrincipal { get; set; }
    public decimal? CumulativePrincipal { get; set; }
    public decimal? PrincipalBalance { get; set; }

    public decimal? PeriodInterest { get; set; }
    public decimal? CumulativeInterest { get; set; }
    public decimal? InterestBalance { get; set; }
}