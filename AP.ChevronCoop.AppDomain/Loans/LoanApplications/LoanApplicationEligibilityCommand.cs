using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;

public class LoanApplicationEligibilityCommand : IRequest<CommandResult<LoanApplicationEligibilityViewModel>>
{
  public string CustomerId { get; set; }
  public string LoanProductId { get; set; }
  public decimal Amount { get; set; }
}