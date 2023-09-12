using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class QueryLoanApplicationGuarantorCommand : IRequest<CommandResult<IQueryable<LoanApplicationGuarantor>>>
{
}