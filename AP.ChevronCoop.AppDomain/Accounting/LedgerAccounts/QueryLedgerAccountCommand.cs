using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts
{

    public class QueryLedgerAccountCommand : IRequest<CommandResult<IQueryable<LedgerAccount>>>
    {

    }



}
