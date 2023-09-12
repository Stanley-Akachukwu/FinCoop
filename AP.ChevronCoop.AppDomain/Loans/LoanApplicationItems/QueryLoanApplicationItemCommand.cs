using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplicationItems;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;

public class QueryLoanApplicationItemCommand : IRequest<CommandResult<IQueryable<LoanApplicationItem>>>
{
}