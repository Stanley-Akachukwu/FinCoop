using AP.ChevronCoop.AppDomain.Documents.OfficeDocuments;
using AP.ChevronCoop.Entities.Documents.OfficeDocuments;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Documents.OfficeDocuments;

public class OfficeDocumentMapperProfile : Profile
{

    public OfficeDocumentMapperProfile()
    {

        CreateMap<OfficeDocument, OfficeDocumentViewModel>().ReverseMap();
        CreateMap<OfficeDocument, CreateOfficeDocumentCommand>().ReverseMap();
        CreateMap<OfficeDocument, UpdateOfficeDocumentCommand>().ReverseMap();
        CreateMap<OfficeDocument, OfficeDocumentMasterView>().ReverseMap();
        CreateMap<OfficeDocumentViewModel, OfficeDocumentMasterView>().ReverseMap();
        CreateMap<CreateOfficeDocumentCommand, OfficeDocumentMasterView>().ReverseMap();
        CreateMap<UpdateOfficeDocumentCommand, OfficeDocumentMasterView>().ReverseMap();




    }
}

