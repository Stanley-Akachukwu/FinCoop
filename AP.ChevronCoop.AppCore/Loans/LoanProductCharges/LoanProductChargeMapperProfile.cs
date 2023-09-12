using AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;
using AP.ChevronCoop.Entities.Loans.LoanProductCharges;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanProductCharges;

public class LoanProductChargeMapperProfile : Profile
{
  public LoanProductChargeMapperProfile()
  {
    CreateMap<LoanProductCharge, LoanProductChargeViewModel>().ReverseMap();
    CreateMap<LoanProductCharge, CreateLoanProductChargeCommand>().ReverseMap();
    CreateMap<LoanProductCharge, UpdateLoanProductChargeCommand>().ReverseMap();
    CreateMap<LoanProductCharge, LoanProductChargeMasterView>().ReverseMap();
    CreateMap<LoanProductChargeViewModel, LoanProductChargeMasterView>().ReverseMap();
    CreateMap<CreateLoanProductChargeCommand, LoanProductChargeMasterView>().ReverseMap();
    CreateMap<UpdateLoanProductChargeCommand, LoanProductChargeMasterView>().ReverseMap();
  }
}