using AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBankAccounts;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Customers.CustomerBankAccounts;

public class CustomerBankAccountMapperProfile : Profile
{
  public CustomerBankAccountMapperProfile()
  {
    CreateMap<CustomerBankAccount, CustomerBankAccountViewModel>().ReverseMap();
    CreateMap<CustomerBankAccount, CreateCustomerBankAccountCommand>().ReverseMap();
    CreateMap<CustomerBankAccount, UpdateCustomerBankAccountCommand>().ReverseMap();
    CreateMap<CustomerBankAccount, CustomerBankAccountMasterView>().ReverseMap();
    CreateMap<CustomerBankAccountViewModel, CustomerBankAccountMasterView>().ReverseMap();
    CreateMap<CreateCustomerBankAccountCommand, CustomerBankAccountMasterView>().ReverseMap();
    CreateMap<UpdateCustomerBankAccountCommand, CustomerBankAccountMasterView>().ReverseMap();
    
    
    CreateMap<CustomerBankAccount, MemberBankAccount>().ReverseMap();
  }
}