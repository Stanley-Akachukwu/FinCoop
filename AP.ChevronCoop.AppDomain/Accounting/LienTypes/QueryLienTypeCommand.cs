using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.LienTypes;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.LienTypes
{
    public class QueryLienTypeCommand : IRequest<CommandResult<IQueryable<LienType>>>
    {

    }







}
