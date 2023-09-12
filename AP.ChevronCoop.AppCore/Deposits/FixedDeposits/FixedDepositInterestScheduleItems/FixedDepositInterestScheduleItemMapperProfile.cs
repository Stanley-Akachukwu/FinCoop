
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;
public class FixedDepositInterestScheduleItemMapperProfile : Profile
{

    public FixedDepositInterestScheduleItemMapperProfile()
    {

        CreateMap<FixedDepositInterestScheduleItem, FixedDepositInterestScheduleItemViewModel>().ReverseMap();
        CreateMap<FixedDepositInterestScheduleItem, CreateFixedDepositInterestScheduleItemCommand>().ReverseMap();
        CreateMap<FixedDepositInterestScheduleItem, UpdateFixedDepositInterestScheduleItemCommand>().ReverseMap();
        CreateMap<FixedDepositInterestScheduleItem, FixedDepositInterestScheduleItemMasterView>().ReverseMap();
        CreateMap<FixedDepositInterestScheduleItemViewModel, FixedDepositInterestScheduleItemMasterView>().ReverseMap();
        CreateMap<CreateFixedDepositInterestScheduleItemCommand, FixedDepositInterestScheduleItemMasterView>().ReverseMap();
        CreateMap<UpdateFixedDepositInterestScheduleItemCommand, FixedDepositInterestScheduleItemMasterView>().ReverseMap();




    }
}
