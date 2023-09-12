using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Documents;
using AP.ChevronCoop.Entities.Documents.DocumentTypes;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Documents.DocumentTypes
{
    public class QueryDocumentTypeCommand : IRequest<CommandResult<IQueryable<DocumentType>>>
    {

    }







}
