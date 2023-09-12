using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursementCharges;

public class UpdateLoanDisbursementChargeCommand : UpdateCommand,
  IRequest<CommandResult<LoanDisbursementChargeViewModel>>
{
  public string LoanDisbursementId { get; set; }
  public string DisbursementChargeId { get; set; }
  public string ChargeType { get; set; }
  public decimal TotalCharge { get; set; }
}