using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsIncreaseDecreases;

public class SavingsIncreaseDecreaseMapperProfile : Profile
{

    public SavingsIncreaseDecreaseMapperProfile()
    {

        CreateMap<SavingsIncreaseDecrease, SavingsIncreaseDecreaseViewModel>().ReverseMap();
        CreateMap<SavingsIncreaseDecrease, CreateSavingsIncreaseDecreaseCommand>().ReverseMap();
        CreateMap<SavingsIncreaseDecrease, UpdateSavingsIncreaseDecreaseCommand>().ReverseMap();
        CreateMap<SavingsIncreaseDecrease, SavingsIncreaseDecreaseMasterView>().ReverseMap();
        CreateMap<SavingsIncreaseDecreaseViewModel, SavingsIncreaseDecreaseMasterView>().ReverseMap();
        CreateMap<CreateSavingsIncreaseDecreaseCommand, SavingsIncreaseDecreaseMasterView>().ReverseMap();
        CreateMap<UpdateSavingsIncreaseDecreaseCommand, SavingsIncreaseDecreaseMasterView>().ReverseMap();




    }
}
