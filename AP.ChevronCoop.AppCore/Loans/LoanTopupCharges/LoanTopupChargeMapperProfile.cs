using AP.ChevronCoop.AppDomain.Loans.LoanTopupCharges;
using AP.ChevronCoop.Entities.Loans.LoanTopupCharges;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopupCharges;

public class LoanTopupChargeMapperProfile : Profile
{
  public LoanTopupChargeMapperProfile()
  {
    CreateMap<LoanTopupCharge, LoanTopupChargeViewModel>().ReverseMap();
    CreateMap<LoanTopupCharge, CreateLoanTopupChargeCommand>().ReverseMap();
    CreateMap<LoanTopupCharge, UpdateLoanTopupChargeCommand>().ReverseMap();
    CreateMap<LoanTopupCharge, LoanTopupChargeMasterView>().ReverseMap();
    CreateMap<LoanTopupChargeViewModel, LoanTopupChargeMasterView>().ReverseMap();
    CreateMap<CreateLoanTopupChargeCommand, LoanTopupChargeMasterView>().ReverseMap();
    CreateMap<UpdateLoanTopupChargeCommand, LoanTopupChargeMasterView>().ReverseMap();
  }
}