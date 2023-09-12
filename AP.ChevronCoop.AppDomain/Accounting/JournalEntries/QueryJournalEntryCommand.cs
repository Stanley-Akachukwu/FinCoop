using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.JournalEntries
{
    public class QueryJournalEntryCommand : IRequest<CommandResult<IQueryable<JournalEntry>>>
    {

    }







}
