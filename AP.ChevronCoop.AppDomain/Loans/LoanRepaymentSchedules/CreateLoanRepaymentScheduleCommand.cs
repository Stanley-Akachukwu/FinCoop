using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepaymentSchedules;

public class CreateLoanRepaymentScheduleCommand : IRequest<CommandResult<List<LoanRepaymentScheduleViewModel>>>
{
  public string TenureUnit { get; set; }
  public decimal TenureValue { get; set; }
  public DateTime CommencementDate { get; set; }
  public decimal Amount { get; set; }
  public decimal Interest { get; set; }
}