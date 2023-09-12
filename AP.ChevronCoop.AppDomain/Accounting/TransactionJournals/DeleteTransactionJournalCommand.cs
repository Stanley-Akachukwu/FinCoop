using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionJournals
{
    public partial class DeleteTransactionJournalCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }







}
