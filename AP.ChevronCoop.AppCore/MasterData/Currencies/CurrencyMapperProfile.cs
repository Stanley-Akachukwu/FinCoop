using AP.ChevronCoop.AppDomain.MasterData.Currencies;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.MasterData.Currencies;

public class CurrencyMapperProfile : Profile
{

    public CurrencyMapperProfile()
    {

        CreateMap<Currency, CurrencyViewModel>().ReverseMap();
        CreateMap<Currency, CreateCurrencyCommand>().ReverseMap();
        CreateMap<Currency, UpdateCurrencyCommand>().ReverseMap();
        CreateMap<Currency, CurrencyMasterView>().ReverseMap();
        CreateMap<CurrencyViewModel, CurrencyMasterView>().ReverseMap();
        CreateMap<CreateCurrencyCommand, CurrencyMasterView>().ReverseMap();
        CreateMap<UpdateCurrencyCommand, CurrencyMasterView>().ReverseMap();




    }
}

