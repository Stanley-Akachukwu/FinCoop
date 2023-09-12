using AP.ChevronCoop.AppDomain.Loans.LoanRepayments;
using AP.ChevronCoop.Entities.Loans.LoanRepayment;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepayments;

public class LoanRepaymentMapperProfile : Profile
{
  public LoanRepaymentMapperProfile()
  {
    CreateMap<LoanRepayment, LoanRepaymentViewModel>().ReverseMap();
    CreateMap<LoanRepayment, CreateLoanRepaymentCommand>().ReverseMap();
    CreateMap<LoanRepayment, UpdateLoanRepaymentCommand>().ReverseMap();
    CreateMap<LoanRepayment, LoanRepaymentMasterView>().ReverseMap();
    CreateMap<LoanRepaymentViewModel, LoanRepaymentMasterView>().ReverseMap();
    CreateMap<CreateLoanRepaymentCommand, LoanRepaymentMasterView>().ReverseMap();
    CreateMap<UpdateLoanRepaymentCommand, LoanRepaymentMasterView>().ReverseMap();
  }
}