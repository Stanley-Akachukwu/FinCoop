using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public class QueryLoanProductCommand : IRequest<CommandResult<IQueryable<LoanProduct>>>
{
}