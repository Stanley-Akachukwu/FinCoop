using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Documents.CustomerDocuments
{
    public class QueryCustomerPaymentDocumentCommand : IRequest<CommandResult<IQueryable<CustomerPaymentDocument>>>
    {

    }
}
