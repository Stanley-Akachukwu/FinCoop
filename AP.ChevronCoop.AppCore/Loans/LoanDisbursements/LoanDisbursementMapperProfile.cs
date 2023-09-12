using AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanDisbursements;

public class LoanDisbursementMapperProfile : Profile
{
  public LoanDisbursementMapperProfile()
  {
    CreateMap<LoanDisbursement, LoanDisbursementViewModel>().ReverseMap();
    CreateMap<LoanDisbursement, CreateLoanDisbursementCommand>().ReverseMap();
    CreateMap<LoanDisbursement, UpdateLoanDisbursementCommand>().ReverseMap();
    CreateMap<LoanDisbursement, LoanDisbursementMasterView>().ReverseMap();
    CreateMap<LoanDisbursementViewModel, LoanDisbursementMasterView>().ReverseMap();
    CreateMap<CreateLoanDisbursementCommand, LoanDisbursementMasterView>().ReverseMap();
    CreateMap<UpdateLoanDisbursementCommand, LoanDisbursementMasterView>().ReverseMap();
  }
}