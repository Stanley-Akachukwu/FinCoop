using AP.ChevronCoop.AppDomain.Documents.OfficeSheets;
using AP.ChevronCoop.Entities.Documents.OfficeSheets;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Documents.OfficeSheets;


public class OfficeSheetMapperProfile : Profile
{

    public OfficeSheetMapperProfile()
    {

        CreateMap<OfficeSheet, OfficeSheetViewModel>().ReverseMap();
        CreateMap<OfficeSheet, CreateOfficeSheetCommand>().ReverseMap();
        CreateMap<OfficeSheet, UpdateOfficeSheetCommand>().ReverseMap();
        CreateMap<OfficeSheet, OfficeSheetMasterView>().ReverseMap();
        CreateMap<OfficeSheetViewModel, OfficeSheetMasterView>().ReverseMap();
        CreateMap<CreateOfficeSheetCommand, OfficeSheetMasterView>().ReverseMap();
        CreateMap<UpdateOfficeSheetCommand, OfficeSheetMasterView>().ReverseMap();




    }
}
