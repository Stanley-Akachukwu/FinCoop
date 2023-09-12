using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;

public class SpecialDepositIncreaseDecreaseMapperProfile : Profile
{

    public SpecialDepositIncreaseDecreaseMapperProfile()
    {

        CreateMap<SpecialDepositIncreaseDecrease, SpecialDepositIncreaseDecreaseViewModel>().ReverseMap();
        CreateMap<SpecialDepositIncreaseDecrease, CreateSpecialDepositIncreaseDecreaseCommand>().ReverseMap();
        CreateMap<SpecialDepositIncreaseDecrease, UpdateSpecialDepositIncreaseDecreaseCommand>().ReverseMap();
        CreateMap<SpecialDepositIncreaseDecrease, SpecialDepositIncreaseDecreaseMasterView>().ReverseMap();
        CreateMap<SpecialDepositIncreaseDecreaseViewModel, SpecialDepositIncreaseDecreaseMasterView>().ReverseMap();
        CreateMap<CreateSpecialDepositIncreaseDecreaseCommand, SpecialDepositIncreaseDecreaseMasterView>().ReverseMap();
        CreateMap<UpdateSpecialDepositIncreaseDecreaseCommand, SpecialDepositIncreaseDecreaseMasterView>().ReverseMap();




    }
}
