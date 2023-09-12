

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AutoMapper;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public class SpecialDepositCashAdditionMapperProfile : Profile
    	{

        public SpecialDepositCashAdditionMapperProfile()
        {

		CreateMap<SpecialDepositCashAddition, SpecialDepositCashAdditionViewModel>().ReverseMap(); 
         CreateMap<SpecialDepositCashAddition, CreateSpecialDepositCashAdditionCommand>().ReverseMap(); 
         CreateMap<SpecialDepositCashAddition, UpdateSpecialDepositCashAdditionCommand>().ReverseMap(); 
         CreateMap<SpecialDepositCashAddition, SpecialDepositCashAdditionMasterView>().ReverseMap(); 
         CreateMap<SpecialDepositCashAdditionViewModel, SpecialDepositCashAdditionMasterView>().ReverseMap(); 
         CreateMap<CreateSpecialDepositCashAdditionCommand, SpecialDepositCashAdditionMasterView>().ReverseMap(); 
         CreateMap<UpdateSpecialDepositCashAdditionCommand, SpecialDepositCashAdditionMasterView>().ReverseMap(); 
 
 
 

        }
   	 }
    }
