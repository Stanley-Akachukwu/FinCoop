using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.TransactionDocuments;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionDocuments
{
    public class QueryTransactionDocumentCommand : IRequest<CommandResult<IQueryable<TransactionDocument>>>
    {

    }







}
