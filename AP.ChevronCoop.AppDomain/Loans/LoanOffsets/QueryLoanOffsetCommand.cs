using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanOffsets;

public class QueryLoanOffsetCommand : IRequest<CommandResult<IQueryable<LoanOffset>>>
{
}