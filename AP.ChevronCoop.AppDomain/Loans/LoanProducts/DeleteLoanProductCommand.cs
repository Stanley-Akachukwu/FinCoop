using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public class DeleteLoanProductCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}