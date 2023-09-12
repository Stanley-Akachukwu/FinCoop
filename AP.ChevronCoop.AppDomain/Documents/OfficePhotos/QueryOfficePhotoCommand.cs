using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Documents;
using AP.ChevronCoop.Entities.Documents.OfficePhotos;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Documents.OfficePhotos
{
    public class QueryOfficePhotoCommand : IRequest<CommandResult<IQueryable<OfficePhoto>>>
    {

    }







}
