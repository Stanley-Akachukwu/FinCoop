using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;

public class DeleteLoanApplicationItemCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}