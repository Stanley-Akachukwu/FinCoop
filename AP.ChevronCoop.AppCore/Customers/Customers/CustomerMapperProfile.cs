    using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
    using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
    using AutoMapper;

namespace AP.ChevronCoop.AppCore.Customers.Customers
{
    public class CustomerMapperProfile : Profile
    {

        public CustomerMapperProfile()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
            CreateMap<Customer, CustomerMasterView>().ReverseMap();
            CreateMap<CustomerViewModel, CustomerMasterView>().ReverseMap();
            CreateMap<CreateCustomerCommand, CustomerMasterView>().ReverseMap();
            CreateMap<UpdateCustomerCommand, CustomerMasterView>().ReverseMap();

            CreateMap<Customer, MemberProfile>().ReverseMap();
        }
    }


}