using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanAccounts;

public class DeleteLoanAccountCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}