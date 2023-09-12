using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepayments;

public class DeleteLoanRepaymentCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}