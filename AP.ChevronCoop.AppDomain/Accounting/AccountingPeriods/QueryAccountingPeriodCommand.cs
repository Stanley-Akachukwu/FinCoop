using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.AccountingPeriods;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.AccountingPeriods
{
    public class QueryAccountingPeriodCommand : IRequest<CommandResult<IQueryable<AccountingPeriod>>>
    {

    }







}
