using AP.ChevronCoop.AppDomain.Loans.LoanOffsetCharges;
using AP.ChevronCoop.AppDomain.Loans.LoanOffSetCharges;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanOffSetCharges;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffSetCharges;

public class LoanOffSetChargeMapperProfile : Profile
{
  public LoanOffSetChargeMapperProfile()
  {
    CreateMap<LoanOffSetCharge, LoanOffSetChargeViewModel>().ReverseMap();
    CreateMap<LoanOffSetCharge, CreateLoanOffSetChargeCommand>().ReverseMap();
    CreateMap<LoanOffSetCharge, UpdateLoanOffSetChargeCommand>().ReverseMap();
    CreateMap<LoanOffSetCharge, LoanOffSetChargeMasterView>().ReverseMap();
    CreateMap<LoanOffSetChargeViewModel, LoanOffSetChargeMasterView>().ReverseMap();
    CreateMap<CreateLoanOffSetChargeCommand, LoanOffSetChargeMasterView>().ReverseMap();
    CreateMap<UpdateLoanOffSetChargeCommand, LoanOffSetChargeMasterView>().ReverseMap();
  }
}