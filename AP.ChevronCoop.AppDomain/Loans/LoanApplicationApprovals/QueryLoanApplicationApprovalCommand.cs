using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplicationApprovals;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationApprovals;

public class QueryLoanApplicationApprovalCommand : IRequest<CommandResult<IQueryable<LoanApplicationApproval>>>
{
}