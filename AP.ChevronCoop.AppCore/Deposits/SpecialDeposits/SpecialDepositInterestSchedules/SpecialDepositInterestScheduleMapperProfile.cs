

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AutoMapper;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{
    public class SpecialDepositInterestScheduleMapperProfile : Profile
    	{

        public SpecialDepositInterestScheduleMapperProfile()
        {
		CreateMap<SpecialDepositInterestSchedule, SpecialDepositInterestScheduleViewModel>().ReverseMap(); 
         CreateMap<SpecialDepositInterestSchedule, CreateSpecialDepositInterestScheduleCommand>().ReverseMap(); 
         CreateMap<SpecialDepositInterestSchedule, UpdateSpecialDepositInterestScheduleCommand>().ReverseMap(); 
         CreateMap<SpecialDepositInterestSchedule, SpecialDepositInterestScheduleMasterView>().ReverseMap(); 
         CreateMap<SpecialDepositInterestScheduleViewModel, SpecialDepositInterestScheduleMasterView>().ReverseMap(); 
         CreateMap<CreateSpecialDepositInterestScheduleCommand, SpecialDepositInterestScheduleMasterView>().ReverseMap(); 
         CreateMap<UpdateSpecialDepositInterestScheduleCommand, SpecialDepositInterestScheduleMasterView>().ReverseMap(); 
        }
   	 }
}
