using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public class SpecialDepositWithdrawalMapperProfile : Profile
    	{
        public SpecialDepositWithdrawalMapperProfile()
        {
		CreateMap<SpecialDepositWithdrawal, SpecialDepositWithdrawalViewModel>().ReverseMap(); 
         CreateMap<SpecialDepositWithdrawal, CreateSpecialDepositWithdrawalCommand>().ReverseMap(); 
         CreateMap<SpecialDepositWithdrawal, UpdateSpecialDepositWithdrawalCommand>().ReverseMap(); 
         CreateMap<SpecialDepositWithdrawal, SpecialDepositWithdrawalMasterView>().ReverseMap(); 
         CreateMap<SpecialDepositWithdrawalViewModel, SpecialDepositWithdrawalMasterView>().ReverseMap(); 
         CreateMap<CreateSpecialDepositWithdrawalCommand, SpecialDepositWithdrawalMasterView>().ReverseMap(); 
         CreateMap<UpdateSpecialDepositWithdrawalCommand, SpecialDepositWithdrawalMasterView>().ReverseMap(); 
        }
   	 }
}
