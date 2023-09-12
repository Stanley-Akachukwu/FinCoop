using AP.ChevronCoop.AppDomain.Documents.OfficePhotos;
using AP.ChevronCoop.Entities.Documents.OfficePhotos;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Documents.OfficePhotos;

public class OfficePhotoMapperProfile : Profile
{

    public OfficePhotoMapperProfile()
    {

        CreateMap<OfficePhoto, OfficePhotoViewModel>().ReverseMap();
        CreateMap<OfficePhoto, CreateOfficePhotoCommand>().ReverseMap();
        CreateMap<OfficePhoto, UpdateOfficePhotoCommand>().ReverseMap();
        CreateMap<OfficePhoto, OfficePhotoMasterView>().ReverseMap();
        CreateMap<OfficePhotoViewModel, OfficePhotoMasterView>().ReverseMap();
        CreateMap<CreateOfficePhotoCommand, OfficePhotoMasterView>().ReverseMap();
        CreateMap<UpdateOfficePhotoCommand, OfficePhotoMasterView>().ReverseMap();




    }
}

