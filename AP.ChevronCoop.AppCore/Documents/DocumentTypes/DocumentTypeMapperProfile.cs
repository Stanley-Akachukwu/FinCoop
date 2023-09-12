using AP.ChevronCoop.AppDomain.Documents.DocumentTypes;
using AP.ChevronCoop.Entities.Documents.DocumentTypes;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Documents.DocumentTypes;

public class DocumentTypeMapperProfile : Profile
{

    public DocumentTypeMapperProfile()
    {

        CreateMap<DocumentType, DocumentTypeViewModel>().ReverseMap();
        CreateMap<DocumentType, CreateDocumentTypeCommand>().ReverseMap();
        CreateMap<DocumentType, UpdateDocumentTypeCommand>().ReverseMap();
        CreateMap<DocumentType, DocumentTypeMasterView>().ReverseMap();
        CreateMap<DocumentTypeViewModel, DocumentTypeMasterView>().ReverseMap();
        CreateMap<CreateDocumentTypeCommand, DocumentTypeMasterView>().ReverseMap();
        CreateMap<UpdateDocumentTypeCommand, DocumentTypeMasterView>().ReverseMap();




    }
}
