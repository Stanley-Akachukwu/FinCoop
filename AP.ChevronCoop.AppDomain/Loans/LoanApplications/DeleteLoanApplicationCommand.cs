using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;

public class DeleteLoanApplicationCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}