using AP.ChevronCoop.AppDomain.Customers.CustomerBeneficiaries;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBeneficiaries;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Customers.CustomerBeneficiaries;

public class CustomerBeneficiaryMapperProfile : Profile
{
    public CustomerBeneficiaryMapperProfile()
    {
        CreateMap<CustomerBeneficiary, CustomerBeneficiaryViewModel>().ReverseMap();
        CreateMap<CustomerBeneficiary, CreateCustomerBeneficiaryCommand>().ReverseMap();
        CreateMap<CustomerBeneficiary, UpdateCustomerBeneficiaryCommand>().ReverseMap();
        CreateMap<CustomerBeneficiary, CustomerBeneficiaryMasterView>().ReverseMap();
        CreateMap<CustomerBeneficiaryViewModel, CustomerBeneficiaryMasterView>().ReverseMap();
        CreateMap<CreateCustomerBeneficiaryCommand, CustomerBeneficiaryMasterView>().ReverseMap();
        CreateMap<UpdateCustomerBeneficiaryCommand, CustomerBeneficiaryMasterView>().ReverseMap();
        
        
        CreateMap<CustomerBeneficiary, MemberBeneficiary>().ReverseMap();
    }
}