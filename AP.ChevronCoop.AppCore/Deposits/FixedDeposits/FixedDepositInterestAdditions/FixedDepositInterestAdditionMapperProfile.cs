
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestAdditions;
public class FixedDepositInterestAdditionMapperProfile : Profile
{

    public FixedDepositInterestAdditionMapperProfile()
    {

        CreateMap<FixedDepositInterestAddition, FixedDepositInterestAdditionViewModel>().ReverseMap();
        CreateMap<FixedDepositInterestAddition, CreateFixedDepositInterestAdditionCommand>().ReverseMap();
        CreateMap<FixedDepositInterestAddition, UpdateFixedDepositInterestAdditionCommand>().ReverseMap();
        CreateMap<FixedDepositInterestAddition, FixedDepositInterestAdditionMasterView>().ReverseMap();
        CreateMap<FixedDepositInterestAdditionViewModel, FixedDepositInterestAdditionMasterView>().ReverseMap();
        CreateMap<CreateFixedDepositInterestAdditionCommand, FixedDepositInterestAdditionMasterView>().ReverseMap();
        CreateMap<UpdateFixedDepositInterestAdditionCommand, FixedDepositInterestAdditionMasterView>().ReverseMap();




    }
}
