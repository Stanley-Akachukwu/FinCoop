using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Loans.LoanOffsetCharges;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanOffSetCharges;

public class UpdateLoanOffSetChargeCommand : UpdateCommand, IRequest<CommandResult<LoanOffSetChargeViewModel>>
{
  public string LoanOffsetId { get; set; }

  public string OffsetChargeId { get; set; }

  public string ChargeType { get; set; }

  public decimal TotalCharge { get; set; }
}