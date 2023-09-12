using AP.ChevronCoop.AppDomain.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepaymentSchedules;

public class LoanRepaymentScheduleMapperProfile : Profile
{
  public LoanRepaymentScheduleMapperProfile()
  {
    // CreateMap<LoanRepaymentScheduleItem, LoanRepaymentScheduleViewModel>().ReverseMap();
    // CreateMap<LoanRepaymentScheduleItem, CreateLoanRepaymentScheduleCommand>().ReverseMap();
    // CreateMap<LoanRepaymentScheduleItem, UpdateLoanRepaymentScheduleCommand>().ReverseMap();
    // CreateMap<LoanRepaymentScheduleItem, LoanRepaymentScheduleMasterView>().ReverseMap();
    CreateMap<LoanRepaymentScheduleViewModel, LoanRepaymentScheduleMasterView>().ReverseMap();
    CreateMap<CreateLoanRepaymentScheduleCommand, LoanRepaymentScheduleMasterView>().ReverseMap();
    CreateMap<UpdateLoanRepaymentScheduleCommand, LoanRepaymentScheduleMasterView>().ReverseMap();
  }
}