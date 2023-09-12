using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationApprovals;

public class CreateLoanApplicationApprovalCommand : CreateCommand,
  IRequest<CommandResult<LoanApplicationApprovalViewModel>>
{
  public string Status { get; set; }

  public string LoanApplicationId { get; set; }
}