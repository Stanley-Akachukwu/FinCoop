using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanRepayment;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepaymentCharges;

public class QueryLoanRepaymentChargeCommand : IRequest<CommandResult<IQueryable<LoanRepaymentCharge>>>
{
}