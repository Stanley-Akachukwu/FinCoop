using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.LedgerAccounts
{
    public class LedgerAccountMapperProfile : Profile
    {
        public LedgerAccountMapperProfile()
        {
            CreateMap<LedgerAccount, LedgerAccountViewModel>().ReverseMap();
            CreateMap<LedgerAccount, CreateLedgerAccountCommand>().ReverseMap();
            CreateMap<LedgerAccount, UpdateLedgerAccountCommand>().ReverseMap();
            CreateMap<LedgerAccount, LedgerAccountMasterView>().ReverseMap();
            CreateMap<LedgerAccountViewModel, LedgerAccountMasterView>().ReverseMap();
            CreateMap<CreateLedgerAccountCommand, LedgerAccountMasterView>().ReverseMap();
            CreateMap<UpdateLedgerAccountCommand, LedgerAccountMasterView>().ReverseMap();
        }
    }
}
