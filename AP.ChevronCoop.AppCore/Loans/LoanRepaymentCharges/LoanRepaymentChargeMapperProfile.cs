using AP.ChevronCoop.AppDomain.Loans.LoanRepaymentCharges;
using AP.ChevronCoop.Entities.Loans.LoanRepayment;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentCharges;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepaymentCharges;

public class LoanRepaymentChargeMapperProfile : Profile
{
  public LoanRepaymentChargeMapperProfile()
  {
    CreateMap<LoanRepaymentCharge, LoanRepaymentChargeViewModel>().ReverseMap();
    CreateMap<LoanRepaymentCharge, CreateLoanRepaymentChargeCommand>().ReverseMap();
    CreateMap<LoanRepaymentCharge, UpdateLoanRepaymentChargeCommand>().ReverseMap();
    CreateMap<LoanRepaymentCharge, LoanRepaymentChargeMasterView>().ReverseMap();
    CreateMap<LoanRepaymentChargeViewModel, LoanRepaymentChargeMasterView>().ReverseMap();
    CreateMap<CreateLoanRepaymentChargeCommand, LoanRepaymentChargeMasterView>().ReverseMap();
    CreateMap<UpdateLoanRepaymentChargeCommand, LoanRepaymentChargeMasterView>().ReverseMap();
  }
}