using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;

public class CreateLoanProductChargeCommand : CreateCommand, IRequest<CommandResult<LoanProductChargeViewModel>>
{
  public string ProductId { get; set; }

  public string ChargeType { get; set; }
  public string ChargeId { get; set; }
}