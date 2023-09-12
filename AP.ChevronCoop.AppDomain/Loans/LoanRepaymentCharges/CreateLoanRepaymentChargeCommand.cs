using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepaymentCharges;

public class CreateLoanRepaymentChargeCommand : CreateCommand, IRequest<CommandResult<LoanRepaymentChargeViewModel>>
{
  public string LoanRepaymentId { get; set; }
  public string RepaymentChargeId { get; set; }
  public string ChargeType { get; set; }
  public decimal TotalCharge { get; set; }
}