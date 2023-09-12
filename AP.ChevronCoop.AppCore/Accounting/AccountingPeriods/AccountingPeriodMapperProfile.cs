using AP.ChevronCoop.AppDomain.Accounting.AccountingPeriods;
using AP.ChevronCoop.Entities.Accounting.AccountingPeriods;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.AccountingPeriods
{
    public class AccountingPeriodMapperProfile : Profile
    {

        public AccountingPeriodMapperProfile()
        {

            CreateMap<AccountingPeriod, AccountingPeriodViewModel>().ReverseMap();
            CreateMap<AccountingPeriod, CreateAccountingPeriodCommand>().ReverseMap();
            CreateMap<AccountingPeriod, UpdateAccountingPeriodCommand>().ReverseMap();
            CreateMap<AccountingPeriod, AccountingPeriodMasterView>().ReverseMap();
            CreateMap<AccountingPeriodViewModel, AccountingPeriodMasterView>().ReverseMap();
            CreateMap<CreateAccountingPeriodCommand, AccountingPeriodMasterView>().ReverseMap();
            CreateMap<UpdateAccountingPeriodCommand, AccountingPeriodMasterView>().ReverseMap();
        }
    }
}
