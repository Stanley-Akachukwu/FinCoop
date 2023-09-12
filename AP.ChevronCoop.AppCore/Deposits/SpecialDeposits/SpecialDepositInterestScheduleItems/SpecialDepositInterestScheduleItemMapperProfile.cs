

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AutoMapper;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public class SpecialDepositInterestScheduleItemMapperProfile : Profile
    	{

        public SpecialDepositInterestScheduleItemMapperProfile()
        {
		CreateMap<SpecialDepositInterestScheduleItem, SpecialDepositInterestScheduleItemViewModel>().ReverseMap(); 
         CreateMap<SpecialDepositInterestScheduleItem, CreateSpecialDepositInterestScheduleItemCommand>().ReverseMap(); 
         CreateMap<SpecialDepositInterestScheduleItem, UpdateSpecialDepositInterestScheduleItemCommand>().ReverseMap(); 
         CreateMap<SpecialDepositInterestScheduleItem, SpecialDepositInterestScheduleItemMasterView>().ReverseMap(); 
         CreateMap<SpecialDepositInterestScheduleItemViewModel, SpecialDepositInterestScheduleItemMasterView>().ReverseMap(); 
         CreateMap<CreateSpecialDepositInterestScheduleItemCommand, SpecialDepositInterestScheduleItemMasterView>().ReverseMap(); 
         CreateMap<UpdateSpecialDepositInterestScheduleItemCommand, SpecialDepositInterestScheduleItemMasterView>().ReverseMap(); 
        }
   	 }
}
