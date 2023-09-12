

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AutoMapper;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccountApplications
{
    public class SpecialDepositAccountApplicationMapperProfile : Profile
    	{

        public SpecialDepositAccountApplicationMapperProfile()
        {

		CreateMap<SpecialDepositAccountApplication, SpecialDepositAccountApplicationViewModel>().ReverseMap(); 
 CreateMap<SpecialDepositAccountApplication, CreateSpecialDepositAccountApplicationCommand>().ReverseMap(); 
 CreateMap<SpecialDepositAccountApplication, UpdateSpecialDepositAccountApplicationCommand>().ReverseMap(); 
 CreateMap<SpecialDepositAccountApplication, SpecialDepositAccountApplicationMasterView>().ReverseMap(); 
 CreateMap<SpecialDepositAccountApplicationViewModel, SpecialDepositAccountApplicationMasterView>().ReverseMap(); 
 CreateMap<CreateSpecialDepositAccountApplicationCommand, SpecialDepositAccountApplicationMasterView>().ReverseMap(); 
 CreateMap<UpdateSpecialDepositAccountApplicationCommand, SpecialDepositAccountApplicationMasterView>().ReverseMap(); 
 
 
 

        }
   	 }
    }
