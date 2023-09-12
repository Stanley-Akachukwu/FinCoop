using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Documents;
using AP.ChevronCoop.Entities.Documents.OfficeSheets;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Documents.OfficeSheets
{
    public class QueryOfficeSheetCommand : IRequest<CommandResult<IQueryable<OfficeSheet>>>
    {

    }







}
