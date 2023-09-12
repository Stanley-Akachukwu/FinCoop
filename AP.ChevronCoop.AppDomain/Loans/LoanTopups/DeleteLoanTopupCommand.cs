using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopups;

public class DeleteLoanTopupCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}