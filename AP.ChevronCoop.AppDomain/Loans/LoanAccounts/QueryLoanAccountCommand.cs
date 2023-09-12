using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanAccounts;

public class QueryLoanAccountCommand : IRequest<CommandResult<IQueryable<LoanAccount>>>
{
}