using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccountApplications;

public class SavingsAccountApplicationMapperProfile : Profile
{

    public SavingsAccountApplicationMapperProfile()
    {

        CreateMap<SavingsAccountApplication, SavingsAccountApplicationViewModel>().ReverseMap();
        CreateMap<SavingsAccountApplication, CreateSavingsAccountApplicationCommand>().ReverseMap();
        CreateMap<SavingsAccountApplication, UpdateSavingsAccountApplicationCommand>().ReverseMap();
        CreateMap<SavingsAccountApplication, SavingsAccountApplicationMasterView>().ReverseMap();
        CreateMap<SavingsAccountApplicationViewModel, SavingsAccountApplicationMasterView>().ReverseMap();
        CreateMap<CreateSavingsAccountApplicationCommand, SavingsAccountApplicationMasterView>().ReverseMap();
        CreateMap<UpdateSavingsAccountApplicationCommand, SavingsAccountApplicationMasterView>().ReverseMap();




    }
}
