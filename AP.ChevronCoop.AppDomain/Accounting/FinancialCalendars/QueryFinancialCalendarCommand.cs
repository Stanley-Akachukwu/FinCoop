using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.FinancialCalendars;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.FinancialCalendars
{
    public class QueryFinancialCalendarCommand : IRequest<CommandResult<IQueryable<FinancialCalendar>>>
    {

    }







}
