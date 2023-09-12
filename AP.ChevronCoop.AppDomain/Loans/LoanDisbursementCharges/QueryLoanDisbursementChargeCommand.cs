using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursementCharges;

public class QueryLoanDisbursementChargeCommand : IRequest<CommandResult<IQueryable<LoanDisbursementCharge>>>
{
}