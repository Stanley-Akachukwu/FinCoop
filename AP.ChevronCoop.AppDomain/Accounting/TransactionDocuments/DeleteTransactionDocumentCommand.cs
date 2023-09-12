using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionDocuments
{
    public partial class DeleteTransactionDocumentCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }







}
