using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositAccountApplications;
public class FixedDepositAccountApplicationMapperProfile : Profile
{

    public FixedDepositAccountApplicationMapperProfile()
    {

        CreateMap<FixedDepositAccountApplication, FixedDepositAccountApplicationViewModel>().ReverseMap();
        CreateMap<FixedDepositAccountApplication, CreateFixedDepositAccountApplicationCommand>().ReverseMap();
        CreateMap<FixedDepositAccountApplication, UpdateFixedDepositAccountApplicationCommand>().ReverseMap();
        CreateMap<FixedDepositAccountApplication, FixedDepositAccountApplicationMasterView>().ReverseMap();
        CreateMap<FixedDepositAccountApplicationViewModel, FixedDepositAccountApplicationMasterView>().ReverseMap();
        CreateMap<CreateFixedDepositAccountApplicationCommand, FixedDepositAccountApplicationMasterView>().ReverseMap();
        CreateMap<UpdateFixedDepositAccountApplicationCommand, FixedDepositAccountApplicationMasterView>().ReverseMap();
        CreateMap<CreateFixedDepositAccountApplicationCommand, CustomerPaymentDocument>().ReverseMap();


    }
}
