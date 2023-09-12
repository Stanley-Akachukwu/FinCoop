using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollCronJobConfigs
{
    public class PayrollCronJobConfigMapperProfile : Profile
    {

        public PayrollCronJobConfigMapperProfile()
        {

            CreateMap<PayrollCronJobConfig, CreatePayrollCronJobConfigCommand>().ReverseMap();
            CreateMap<PayrollCronJobConfig, UpdatePayrollCronJobConfigCommand>().ReverseMap();
            CreateMap<PayrollCronJobConfig, PayrollCronJobConfigViewModel>().ReverseMap();

        }
    }

}