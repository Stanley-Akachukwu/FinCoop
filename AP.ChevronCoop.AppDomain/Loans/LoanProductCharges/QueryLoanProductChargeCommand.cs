using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanProductCharges;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;

public class QueryLoanProductChargeCommand : IRequest<CommandResult<IQueryable<LoanProductCharge>>>
{
}