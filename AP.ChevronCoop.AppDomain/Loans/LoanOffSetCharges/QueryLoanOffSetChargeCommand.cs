using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanOffsetCharges;

public class QueryLoanOffSetChargeCommand : IRequest<CommandResult<IQueryable<LoanOffSetCharge>>>
{
}