using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopups;

public class QueryLoanTopupCommand : IRequest<CommandResult<IQueryable<LoanTopup>>>
{
}