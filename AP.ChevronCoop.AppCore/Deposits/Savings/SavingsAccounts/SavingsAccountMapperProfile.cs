
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccounts;

public class SavingsAccountMapperProfile : Profile
{

    public SavingsAccountMapperProfile()
    {
        CreateMap<SavingsAccount, SavingsAccountViewModel>().ReverseMap();
        CreateMap<SavingsAccount, CreateSavingsAccountCommand>().ReverseMap();
        CreateMap<SavingsAccount, UpdateSavingsAccountCommand>().ReverseMap();
        CreateMap<SavingsAccount, SavingsAccountMasterView>().ReverseMap();
        CreateMap<SavingsAccountViewModel, SavingsAccountMasterView>().ReverseMap();
        CreateMap<CreateSavingsAccountCommand, SavingsAccountMasterView>().ReverseMap();
        CreateMap<UpdateSavingsAccountCommand, SavingsAccountMasterView>().ReverseMap();
    }
}
