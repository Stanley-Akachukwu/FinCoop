using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AutoMapper;

namespace ChevronCoop.API.Controllers.Deposits.FixedDeposits.FixedDepositAccounts;

public class FixedDepositAccountMapperProfile : Profile
{

    public FixedDepositAccountMapperProfile()
    {

        CreateMap<FixedDepositAccount, FixedDepositAccountViewModel>().ReverseMap();
        CreateMap<FixedDepositAccount, CreateFixedDepositAccountCommand>().ReverseMap();
        CreateMap<FixedDepositAccount, UpdateFixedDepositAccountCommand>().ReverseMap();
        CreateMap<FixedDepositAccount, FixedDepositAccountMasterView>().ReverseMap();
        CreateMap<FixedDepositAccountViewModel, FixedDepositAccountMasterView>().ReverseMap();
        CreateMap<CreateFixedDepositAccountCommand, FixedDepositAccountMasterView>().ReverseMap();
        CreateMap<UpdateFixedDepositAccountCommand, FixedDepositAccountMasterView>().ReverseMap();




    }
}
