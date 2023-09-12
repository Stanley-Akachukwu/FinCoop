using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsCashAdditions;

public class SavingsCashAdditionMapperProfile : Profile
{

    public SavingsCashAdditionMapperProfile()
    {

        CreateMap<SavingsCashAddition, SavingsCashAdditionViewModel>().ReverseMap();
        CreateMap<SavingsCashAddition, CreateSavingsCashAdditionCommand>().ReverseMap();
        CreateMap<SavingsCashAddition, UpdateSavingsCashAdditionCommand>().ReverseMap();
        CreateMap<SavingsCashAddition, SavingsCashAdditionMasterView>().ReverseMap();
        CreateMap<SavingsCashAdditionViewModel, SavingsCashAdditionMasterView>().ReverseMap();
        CreateMap<CreateSavingsCashAdditionCommand, SavingsCashAdditionMasterView>().ReverseMap();
        CreateMap<UpdateSavingsCashAdditionCommand, SavingsCashAdditionMasterView>().ReverseMap();
        CreateMap<CreateSavingsCashAdditionCommand, CustomerPaymentDocument>().ReverseMap();
    }
}
