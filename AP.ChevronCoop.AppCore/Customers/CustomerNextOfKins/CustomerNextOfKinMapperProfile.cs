using AP.ChevronCoop.AppDomain.Customers.CustomerNextOfKins;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerNextOfKins;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Customers.CustomerNextOfKins;

public class CustomerNextOfKinMapperProfile : Profile
{
    public CustomerNextOfKinMapperProfile()
    {
        CreateMap<CustomerNextOfKin, CustomerNextOfKinViewModel>().ReverseMap();
        CreateMap<CustomerNextOfKin, CreateCustomerNextOfKinCommand>().ReverseMap();
        CreateMap<CustomerNextOfKin, UpdateCustomerNextOfKinCommand>().ReverseMap();
        CreateMap<CustomerNextOfKin, CustomerNextOfKinMasterView>().ReverseMap();
        CreateMap<CustomerNextOfKinViewModel, CustomerNextOfKinMasterView>().ReverseMap();
        CreateMap<CreateCustomerNextOfKinCommand, CustomerNextOfKinMasterView>().ReverseMap();
        CreateMap<UpdateCustomerNextOfKinCommand, CustomerNextOfKinMasterView>().ReverseMap();
        
        
        CreateMap<CustomerNextOfKin, MemberNextOfKin>().ReverseMap();
    }
}