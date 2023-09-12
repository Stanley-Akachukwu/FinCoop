using AP.ChevronCoop.AppDomain.Accounting.FinancialCalendars;
using AP.ChevronCoop.Entities.Accounting.FinancialCalendars;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.FinancialCalendars
{
    public class FinancialCalendarMapperProfile : Profile
    {
        public FinancialCalendarMapperProfile()
        {
            CreateMap<FinancialCalendar, FinancialCalendarViewModel>().ReverseMap();
            CreateMap<FinancialCalendar, CreateFinancialCalendarCommand>().ReverseMap();
            CreateMap<FinancialCalendar, UpdateFinancialCalendarCommand>().ReverseMap();
            CreateMap<FinancialCalendar, FinancialCalendarMasterView>().ReverseMap();
            CreateMap<FinancialCalendarViewModel, FinancialCalendarMasterView>().ReverseMap();
            CreateMap<CreateFinancialCalendarCommand, FinancialCalendarMasterView>().ReverseMap();
            CreateMap<UpdateFinancialCalendarCommand, FinancialCalendarMasterView>().ReverseMap();
        }
    }
}
