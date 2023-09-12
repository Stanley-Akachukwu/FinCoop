using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanAccounts;

public class CreateLoanAccountCommand : CreateCommand, IRequest<CommandResult<LoanAccountViewModel>>
{
  public string LoanApplicationId { get; set; }
}
