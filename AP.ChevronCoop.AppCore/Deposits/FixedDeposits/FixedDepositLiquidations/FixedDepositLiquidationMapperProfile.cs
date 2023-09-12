using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositLiquidations;

public class FixedDepositLiquidationMapperProfile : Profile
{

    public FixedDepositLiquidationMapperProfile()
    {

        CreateMap<FixedDepositLiquidation, FixedDepositLiquidationViewModel>().ReverseMap();
        CreateMap<FixedDepositLiquidation, CreateFixedDepositLiquidationCommand>().ReverseMap();
        CreateMap<FixedDepositLiquidation, UpdateFixedDepositLiquidationCommand>().ReverseMap();
        CreateMap<FixedDepositLiquidation, FixedDepositLiquidationMasterView>().ReverseMap();
        CreateMap<FixedDepositLiquidationViewModel, FixedDepositLiquidationMasterView>().ReverseMap();
        CreateMap<CreateFixedDepositLiquidationCommand, FixedDepositLiquidationMasterView>().ReverseMap();
        CreateMap<UpdateFixedDepositLiquidationCommand, FixedDepositLiquidationMasterView>().ReverseMap();




    }
}
