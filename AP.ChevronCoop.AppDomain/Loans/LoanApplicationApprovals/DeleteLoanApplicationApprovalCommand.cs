using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationApprovals;

public class DeleteLoanApplicationApprovalCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}