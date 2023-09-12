using AP.ChevronCoop.AppDomain.MasterData.Banks;
using AP.ChevronCoop.Entities.MasterData.Banks;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.MasterData.Banks;

public class BankMapperProfile : Profile
{

    public BankMapperProfile()
    {

        CreateMap<Bank, BankViewModel>().ReverseMap();
        CreateMap<Bank, CreateBankCommand>().ReverseMap();
        CreateMap<Bank, UpdateBankCommand>().ReverseMap();
        CreateMap<Bank, BankMasterView>().ReverseMap();
        CreateMap<BankViewModel, BankMasterView>().ReverseMap();
        CreateMap<CreateBankCommand, BankMasterView>().ReverseMap();
        CreateMap<UpdateBankCommand, BankMasterView>().ReverseMap();




    }
}