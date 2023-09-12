using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanOffsetCharges;

public class CreateLoanOffSetChargeCommand : CreateCommand, IRequest<CommandResult<LoanOffSetChargeViewModel>>
{
  public string LoanOffsetId { get; set; }
  public string OffsetChargeId { get; set; }
  public string ChargeType { get; set; }
  public decimal TotalCharge { get; set; }
}