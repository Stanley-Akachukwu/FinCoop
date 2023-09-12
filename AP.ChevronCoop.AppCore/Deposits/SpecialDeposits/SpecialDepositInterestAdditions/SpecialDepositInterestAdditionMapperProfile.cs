

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AutoMapper;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositInterestAdditions
{
    public class SpecialDepositInterestAdditionMapperProfile : Profile
    	{

        public SpecialDepositInterestAdditionMapperProfile()
        {
		CreateMap<SpecialDepositInterestAddition, SpecialDepositInterestAdditionViewModel>().ReverseMap(); 
         CreateMap<SpecialDepositInterestAddition, CreateSpecialDepositInterestAdditionCommand>().ReverseMap(); 
         CreateMap<SpecialDepositInterestAddition, UpdateSpecialDepositInterestAdditionCommand>().ReverseMap(); 
         CreateMap<SpecialDepositInterestAddition, SpecialDepositInterestAdditionMasterView>().ReverseMap(); 
         CreateMap<SpecialDepositInterestAdditionViewModel, SpecialDepositInterestAdditionMasterView>().ReverseMap(); 
         CreateMap<CreateSpecialDepositInterestAdditionCommand, SpecialDepositInterestAdditionMasterView>().ReverseMap(); 
         CreateMap<UpdateSpecialDepositInterestAdditionCommand, SpecialDepositInterestAdditionMasterView>().ReverseMap(); 
        }
   	 }
}
