

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AutoMapper;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public class SpecialDepositAccountMapperProfile : Profile
    	{
        public SpecialDepositAccountMapperProfile()
        {
		 CreateMap<SpecialDepositAccount, SpecialDepositAccountViewModel>().ReverseMap(); 
         CreateMap<SpecialDepositAccount, CreateSpecialDepositAccountCommand>().ReverseMap(); 
         CreateMap<SpecialDepositAccount, UpdateSpecialDepositAccountCommand>().ReverseMap(); 
         CreateMap<SpecialDepositAccount, SpecialDepositAccountMasterView>().ReverseMap(); 
         CreateMap<SpecialDepositAccountViewModel, SpecialDepositAccountMasterView>().ReverseMap(); 
         CreateMap<CreateSpecialDepositAccountCommand, SpecialDepositAccountMasterView>().ReverseMap(); 
         CreateMap<UpdateSpecialDepositAccountCommand, SpecialDepositAccountMasterView>().ReverseMap();
         CreateMap<SpecialDepositAccount, UpdateSpecialDepositDefaultCreatedAccountCommand>().ReverseMap();
        }
    }
}
