

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AutoMapper;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules
{
    public class SpecialDepositAccountDeductionScheduleMapperProfile : Profile
    	{

        public SpecialDepositAccountDeductionScheduleMapperProfile()
        {

		CreateMap<SpecialDepositAccountDeductionSchedule, SpecialDepositAccountDeductionScheduleViewModel>().ReverseMap(); 
         CreateMap<SpecialDepositAccountDeductionSchedule, CreateSpecialDepositAccountDeductionScheduleCommand>().ReverseMap(); 
         CreateMap<SpecialDepositAccountDeductionSchedule, UpdateSpecialDepositAccountDeductionScheduleCommand>().ReverseMap(); 
         CreateMap<SpecialDepositAccountDeductionSchedule, SpecialDepositAccountDeductionScheduleMasterView>().ReverseMap(); 
         CreateMap<SpecialDepositAccountDeductionScheduleViewModel, SpecialDepositAccountDeductionScheduleMasterView>().ReverseMap(); 
         CreateMap<CreateSpecialDepositAccountDeductionScheduleCommand, SpecialDepositAccountDeductionScheduleMasterView>().ReverseMap(); 
         CreateMap<UpdateSpecialDepositAccountDeductionScheduleCommand, SpecialDepositAccountDeductionScheduleMasterView>().ReverseMap(); 
 
 
 

        }
   	 }
    }
