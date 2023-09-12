using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanProductPublications.MemberLoanProductPublications;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.CustomerLoanProductPublications;

public class
  QueryLoanProductPublicationCommand : IRequest<CommandResult<IQueryable<CustomerLoanProductPublication>>>
{
}