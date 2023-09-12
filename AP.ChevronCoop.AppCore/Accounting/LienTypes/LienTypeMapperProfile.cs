using AP.ChevronCoop.AppDomain.Accounting.LienTypes;
using AP.ChevronCoop.Entities.Accounting.LienTypes;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.LienTypes
{
    public class LienTypeMapperProfile : Profile
    {
        public LienTypeMapperProfile()
        {

            CreateMap<LienType, LienTypeViewModel>().ReverseMap();
            CreateMap<LienType, CreateLienTypeCommand>().ReverseMap();
            CreateMap<LienType, UpdateLienTypeCommand>().ReverseMap();
            CreateMap<LienType, LienTypeMasterView>().ReverseMap();
            CreateMap<LienTypeViewModel, LienTypeMasterView>().ReverseMap();
            CreateMap<CreateLienTypeCommand, LienTypeMasterView>().ReverseMap();
            CreateMap<UpdateLienTypeCommand, LienTypeMasterView>().ReverseMap();




        }
    }


}