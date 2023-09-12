using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionScheduleItems;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionScheduleItems;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionScheduleItem;

public class PayrollDeductionScheduleItemMapperProfile : Profile
{
  public PayrollDeductionScheduleItemMapperProfile()
  {
    CreateMap<Entities.Payroll.PayrollDeductionScheduleItem, PayrollDeductionScheduleItemViewModel>().ReverseMap();
    CreateMap<Entities.Payroll.PayrollDeductionScheduleItem, CreatePayrollDeductionScheduleItemCommand>().ReverseMap();
    CreateMap<Entities.Payroll.PayrollDeductionScheduleItem, UpdatePayrollDeductionScheduleItemCommand>().ReverseMap();
    CreateMap<Entities.Payroll.PayrollDeductionScheduleItem, PayrollDeductionScheduleItemMasterView>().ReverseMap();
    CreateMap<PayrollDeductionScheduleItemViewModel, PayrollDeductionScheduleItemMasterView>().ReverseMap();
    CreateMap<CreatePayrollDeductionScheduleItemCommand, PayrollDeductionScheduleItemMasterView>().ReverseMap();
    CreateMap<UpdatePayrollDeductionScheduleItemCommand, PayrollDeductionScheduleItemMasterView>().ReverseMap();
  }
}