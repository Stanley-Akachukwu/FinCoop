using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanRepayment;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepayments;

public class QueryLoanRepaymentCommand : IRequest<CommandResult<IQueryable<LoanRepayment>>>
{
}