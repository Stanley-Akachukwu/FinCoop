using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopupCharges;

public class QueryLoanTopupChargeCommand : IRequest<CommandResult<IQueryable<LoanTopupCharge>>>
{
}