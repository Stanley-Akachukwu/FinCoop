using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedule;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionSchedules;

public class PayrollDeductionScheduleMapperProfile : Profile
{
    public PayrollDeductionScheduleMapperProfile()
    {
        CreateMap<PayrollDeductionSchedule, PayrollDeductionScheduleViewModel>().ReverseMap();
        CreateMap<PayrollDeductionSchedule, CreatePayrollDeductionScheduleCommand>().ReverseMap();
        CreateMap<PayrollDeductionSchedule, UpdatePayrollDeductionScheduleCommand>().ReverseMap();
        CreateMap<PayrollDeductionSchedule, PayrollDeductionScheduleMasterView>().ReverseMap();
        CreateMap<PayrollDeductionScheduleViewModel, PayrollDeductionScheduleMasterView>().ReverseMap();
        CreateMap<CreatePayrollDeductionScheduleCommand, PayrollDeductionScheduleMasterView>().ReverseMap();
        CreateMap<UpdatePayrollDeductionScheduleCommand, PayrollDeductionScheduleMasterView>().ReverseMap();
    }
}

