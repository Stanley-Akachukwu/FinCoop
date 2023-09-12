using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationApprovals;

public class UpdateLoanApplicationApprovalCommand : UpdateCommand,
  IRequest<CommandResult<LoanApplicationApprovalViewModel>>
{
  public string Status { get; set; }
  public string LoanApplicationId { get; set; }
}