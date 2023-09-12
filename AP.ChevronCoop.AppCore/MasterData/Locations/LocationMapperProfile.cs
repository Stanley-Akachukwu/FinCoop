using AP.ChevronCoop.AppDomain.MasterData.Locations;
using AP.ChevronCoop.Entities.MasterData.Locations;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.MasterData.Locations;


public class LocationMapperProfile : Profile
{

    public LocationMapperProfile()
    {

        CreateMap<Location, LocationViewModel>().ReverseMap();
        CreateMap<Location, CreateLocationCommand>().ReverseMap();
        CreateMap<Location, UpdateLocationCommand>().ReverseMap();
        CreateMap<Location, LocationMasterView>().ReverseMap();
        CreateMap<LocationViewModel, LocationMasterView>().ReverseMap();
        CreateMap<CreateLocationCommand, LocationMasterView>().ReverseMap();
        CreateMap<UpdateLocationCommand, LocationMasterView>().ReverseMap();




    }
}
