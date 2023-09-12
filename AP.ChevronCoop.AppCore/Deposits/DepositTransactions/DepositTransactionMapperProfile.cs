using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.DepositTransactions;

public class DepositTransactionMapperProfile : Profile
{
  public DepositTransactionMapperProfile()
  {
    CreateMap<DepositTransactionCommand, DepositTransactionViewModel>().ReverseMap();
  }
}