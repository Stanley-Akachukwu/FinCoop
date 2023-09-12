using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;

public class UpdateLoanApplicationCommand : UpdateCommand, IRequest<CommandResult<LoanApplicationViewModel>>
{
  public string LoanProductId { get; set; }
  public string MemberProfileId { get; set; }
  public decimal Amount { get; set; }
  public string RepaymentTenureUnit { get; set; }
  public int RepaymentPeriod { get; set; }
  public DateTime RepaymentCommencementDate { get; set; }
  public bool UseSpecialDeposit { get; set; }
  public string? SpecialDepositId { get; set; }
  public string? DestinationAccountId { get; set; }
}