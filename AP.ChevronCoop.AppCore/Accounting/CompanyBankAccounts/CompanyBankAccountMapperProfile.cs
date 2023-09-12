using AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.CompanyBankAccounts
{
    public class CompanyBankAccountMapperProfile : Profile
    {
        public CompanyBankAccountMapperProfile()
        {
            CreateMap<CompanyBankAccount, CompanyBankAccountViewModel>().ReverseMap();
            CreateMap<CompanyBankAccount, CreateCompanyBankAccountCommand>().ReverseMap();
            CreateMap<CompanyBankAccount, UpdateCompanyBankAccountCommand>().ReverseMap();
            CreateMap<CompanyBankAccount, CompanyBankAccountMasterView>().ReverseMap();
            CreateMap<CompanyBankAccountViewModel, CompanyBankAccountMasterView>().ReverseMap();
            CreateMap<CreateCompanyBankAccountCommand, CompanyBankAccountMasterView>().ReverseMap();
            CreateMap<UpdateCompanyBankAccountCommand, CompanyBankAccountMasterView>().ReverseMap();
        }
    }
}
