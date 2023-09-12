

using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using AutoMapper;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositFundTransfers
{
    public class SpecialDepositFundTransferMapperProfile : Profile
    	{

        public SpecialDepositFundTransferMapperProfile()
        {
		CreateMap<SpecialDepositFundTransfer, SpecialDepositFundTransferViewModel>().ReverseMap(); 
         CreateMap<SpecialDepositFundTransfer, CreateSpecialDepositFundTransferCommand>().ReverseMap(); 
         CreateMap<SpecialDepositFundTransfer, UpdateSpecialDepositFundTransferCommand>().ReverseMap(); 
         CreateMap<SpecialDepositFundTransfer, SpecialDepositFundTransferMasterView>().ReverseMap(); 
         CreateMap<SpecialDepositFundTransferViewModel, SpecialDepositFundTransferMasterView>().ReverseMap(); 
         CreateMap<CreateSpecialDepositFundTransferCommand, SpecialDepositFundTransferMasterView>().ReverseMap(); 
         CreateMap<UpdateSpecialDepositFundTransferCommand, SpecialDepositFundTransferMasterView>().ReverseMap(); 
        }
   	 }
    }
