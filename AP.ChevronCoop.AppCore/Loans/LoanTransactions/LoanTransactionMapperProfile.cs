using AP.ChevronCoop.AppDomain.Loans.LoanTransactions;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanTransactions;

public class LoanTransactionMapperProfile : Profile
{
  public LoanTransactionMapperProfile()
  {
    CreateMap<LoanTransactionCommand, LoanTransactionViewModel>().ReverseMap();
  }
}