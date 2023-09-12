using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionJournals
{
    public class QueryTransactionJournalCommand : IRequest<CommandResult<IQueryable<TransactionJournal>>>
    {

    }







}
