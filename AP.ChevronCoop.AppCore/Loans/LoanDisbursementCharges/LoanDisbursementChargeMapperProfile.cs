using AP.ChevronCoop.AppDomain.Loans.LoanDisbursementCharges;
using AP.ChevronCoop.Entities.Loans.LoanDisbursementCharges;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanDisbursementCharges;

public class LoanDisbursementChargeMapperProfile : Profile
{
  public LoanDisbursementChargeMapperProfile()
  {
    CreateMap<LoanDisbursementCharge, LoanDisbursementChargeViewModel>().ReverseMap();
    CreateMap<LoanDisbursementCharge, CreateLoanDisbursementChargeCommand>().ReverseMap();
    CreateMap<LoanDisbursementCharge, UpdateLoanDisbursementChargeCommand>().ReverseMap();
    CreateMap<LoanDisbursementCharge, LoanDisbursementChargeMasterView>().ReverseMap();
    CreateMap<LoanDisbursementChargeViewModel, LoanDisbursementChargeMasterView>().ReverseMap();
    CreateMap<CreateLoanDisbursementChargeCommand, LoanDisbursementChargeMasterView>().ReverseMap();
    CreateMap<UpdateLoanDisbursementChargeCommand, LoanDisbursementChargeMasterView>().ReverseMap();
  }
}