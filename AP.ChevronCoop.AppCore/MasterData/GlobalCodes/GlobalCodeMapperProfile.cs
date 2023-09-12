using AP.ChevronCoop.AppDomain.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.MasterData.GlobalCodes;

public class GlobalCodeMapperProfile : Profile
{

    public GlobalCodeMapperProfile()
    {

        CreateMap<GlobalCode, GlobalCodeViewModel>().ReverseMap();
        CreateMap<GlobalCode, CreateGlobalCodeCommand>().ReverseMap();
        CreateMap<GlobalCode, UpdateGlobalCodeCommand>().ReverseMap();
        CreateMap<GlobalCode, GlobalCodeMasterView>().ReverseMap();
        CreateMap<GlobalCodeViewModel, GlobalCodeMasterView>().ReverseMap();
        CreateMap<CreateGlobalCodeCommand, GlobalCodeMasterView>().ReverseMap();
        CreateMap<UpdateGlobalCodeCommand, GlobalCodeMasterView>().ReverseMap();




    }
}
