using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;

public class DeleteLoanDisbursementCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}