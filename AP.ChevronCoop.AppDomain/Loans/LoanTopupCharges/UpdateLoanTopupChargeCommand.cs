using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopupCharges;

public class UpdateLoanTopupChargeCommand : UpdateCommand, IRequest<CommandResult<LoanTopupChargeViewModel>>
{
  public string LoanTopupId { get; set; }
  public string TopupChargeId { get; set; }
  public string ChargeType { get; set; }
  public decimal TotalCharge { get; set; }
}