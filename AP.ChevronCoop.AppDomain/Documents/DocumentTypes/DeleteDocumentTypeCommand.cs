using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Documents.DocumentTypes
{
    public partial class DeleteDocumentTypeCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }







}
