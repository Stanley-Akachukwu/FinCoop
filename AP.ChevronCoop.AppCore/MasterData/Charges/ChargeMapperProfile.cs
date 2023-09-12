using AP.ChevronCoop.AppDomain.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.MasterData.Charges
{
    public class ChargeMapperProfile : Profile
    {

        public ChargeMapperProfile()
        {
            CreateMap<Charge, ChargeViewModel>().ReverseMap();
            CreateMap<Charge, CreateChargeCommand>().ReverseMap();
            CreateMap<Charge, UpdateChargeCommand>().ReverseMap();
            CreateMap<Charge, ChargeMasterView>().ReverseMap();
            CreateMap<ChargeViewModel, ChargeMasterView>().ReverseMap();
            CreateMap<CreateChargeCommand, ChargeMasterView>().ReverseMap();
            CreateMap<UpdateChargeCommand, ChargeMasterView>().ReverseMap();
        }
    }
}
