
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
public class FixedDepositChangeInMaturityMapperProfile : Profile
{

    public FixedDepositChangeInMaturityMapperProfile()
    {

        CreateMap<FixedDepositChangeInMaturity, FixedDepositChangeInMaturityViewModel>().ReverseMap();
        CreateMap<FixedDepositChangeInMaturity, CreateFixedDepositChangeInMaturityCommand>().ReverseMap();
        CreateMap<FixedDepositChangeInMaturity, UpdateFixedDepositChangeInMaturityCommand>().ReverseMap();
        CreateMap<FixedDepositChangeInMaturity, FixedDepositChangeInMaturityMasterView>().ReverseMap();
        CreateMap<FixedDepositChangeInMaturityViewModel, FixedDepositChangeInMaturityMasterView>().ReverseMap();
        CreateMap<CreateFixedDepositChangeInMaturityCommand, FixedDepositChangeInMaturityMasterView>().ReverseMap();
        CreateMap<UpdateFixedDepositChangeInMaturityCommand, FixedDepositChangeInMaturityMasterView>().ReverseMap();




    }
}
