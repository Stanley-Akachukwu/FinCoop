using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepaymentSchedules;

public class UpdateLoanRepaymentScheduleCommand : UpdateCommand, IRequest<CommandResult<LoanRepaymentScheduleViewModel>>
{
  public string LoanApplicationId { get; set; }

  public DateTime RepaymentDate { get; set; }
  public string TenureUnit { get; set; }
  public decimal Principal { get; set; }
  public decimal Interest { get; set; }
  public decimal Balance { get; set; }
}