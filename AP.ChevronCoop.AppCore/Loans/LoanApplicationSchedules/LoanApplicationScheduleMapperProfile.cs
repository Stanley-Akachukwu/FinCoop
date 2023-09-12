using AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationSchedules;

public class LoanApplicationScheduleMapperProfile : Profile
{
  public LoanApplicationScheduleMapperProfile()
  {
    CreateMap<LoanApplicationSchedule, LoanApplicationScheduleViewModel>().ReverseMap()
            .ForMember(x => x.Id, src => src.Ignore());
    CreateMap<LoanApplicationSchedule, CreateLoanApplicationScheduleCommand>().ReverseMap();
    CreateMap<LoanApplicationSchedule, LoanApplicationScheduleMasterView>().ReverseMap();
    CreateMap<LoanApplicationScheduleViewModel, LoanApplicationScheduleMasterView>().ReverseMap();
    CreateMap<CreateLoanApplicationScheduleCommand, LoanApplicationScheduleMasterView>().ReverseMap();
  }
}