using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;

public class QueryLoanApplicationCommand : IRequest<CommandResult<IQueryable<LoanApplication>>>
{
}