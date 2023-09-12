using AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;
using AP.ChevronCoop.Entities.Loans.LoanApplicationItems;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationItems;

public class LoanApplicationItemMapperProfile : Profile
{
  public LoanApplicationItemMapperProfile()
  {
    CreateMap<LoanApplicationItem, LoanApplicationItemViewModel>().ReverseMap();
    CreateMap<LoanApplicationItem, CreateLoanApplicationItemCommand>().ReverseMap();
    CreateMap<LoanApplicationItem, UpdateLoanApplicationItemCommand>().ReverseMap();
    CreateMap<LoanApplicationItem, LoanApplicationItemMasterView>().ReverseMap();
    CreateMap<LoanApplicationItemViewModel, LoanApplicationItemMasterView>().ReverseMap();
    CreateMap<CreateLoanApplicationItemCommand, LoanApplicationItemMasterView>().ReverseMap();
    CreateMap<UpdateLoanApplicationItemCommand, LoanApplicationItemMasterView>().ReverseMap();
  }
}