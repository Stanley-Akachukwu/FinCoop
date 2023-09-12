using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;
using AP.ChevronCoop.Entities.Payroll;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionSchedules;

public class PayrollDeductionItemMapperProfile : Profile
{
    public PayrollDeductionItemMapperProfile()
    {
        CreateMap<PayrollDeductionItem, PayrollDeductionItemViewModel>().ReverseMap();
        CreateMap<PayrollDeductionItem, CreatePayrollDeductionItemCommand>().ReverseMap();
        CreateMap<PayrollDeductionItem, UpdatePayrollDeductionItemCommand>().ReverseMap();
        CreateMap<PayrollDeductionItem, PayrollDeductionItemMasterView>().ReverseMap();
        CreateMap<PayrollDeductionItemViewModel, PayrollDeductionItemMasterView>().ReverseMap();
        CreateMap<CreatePayrollDeductionItemCommand, PayrollDeductionItemMasterView>().ReverseMap();
        CreateMap<UpdatePayrollDeductionItemCommand, PayrollDeductionItemMasterView>().ReverseMap();
    }
}
