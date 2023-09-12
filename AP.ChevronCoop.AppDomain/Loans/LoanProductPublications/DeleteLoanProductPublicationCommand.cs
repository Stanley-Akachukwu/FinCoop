using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.CustomerLoanProductPublications;

public class DeleteLoanProductPublicationCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}