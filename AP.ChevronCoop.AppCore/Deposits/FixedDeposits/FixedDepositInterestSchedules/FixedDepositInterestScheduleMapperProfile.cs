using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositInterestSchedules;
public class FixedDepositInterestScheduleMapperProfile : Profile
{

    public FixedDepositInterestScheduleMapperProfile()
    {

        CreateMap<FixedDepositInterestSchedule, FixedDepositInterestScheduleViewModel>().ReverseMap();
        CreateMap<FixedDepositInterestSchedule, CreateFixedDepositInterestScheduleCommand>().ReverseMap();
        CreateMap<FixedDepositInterestSchedule, UpdateFixedDepositInterestScheduleCommand>().ReverseMap();
        CreateMap<FixedDepositInterestSchedule, FixedDepositInterestScheduleMasterView>().ReverseMap();
        CreateMap<FixedDepositInterestScheduleViewModel, FixedDepositInterestScheduleMasterView>().ReverseMap();
        CreateMap<CreateFixedDepositInterestScheduleCommand, FixedDepositInterestScheduleMasterView>().ReverseMap();
        CreateMap<UpdateFixedDepositInterestScheduleCommand, FixedDepositInterestScheduleMasterView>().ReverseMap();




    }
}
