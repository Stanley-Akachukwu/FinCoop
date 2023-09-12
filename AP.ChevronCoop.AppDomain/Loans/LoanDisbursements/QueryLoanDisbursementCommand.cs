using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;

public class QueryLoanDisbursementCommand : IRequest<CommandResult<IQueryable<LoanDisbursement>>>
{
}