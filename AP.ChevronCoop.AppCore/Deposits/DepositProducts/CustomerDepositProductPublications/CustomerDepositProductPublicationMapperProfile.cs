using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.CustomerDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.CustomerDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.CustomerDepositProductPublications
{
    public class CustomerDepositProductPublicationMapperProfile : Profile
    {

        public CustomerDepositProductPublicationMapperProfile()
        {

            CreateMap<CustomerDepositProductPublication, CustomerDepositProductPublicationViewModel>().ReverseMap();
            CreateMap<CustomerDepositProductPublication, CreateCustomerDepositProductPublicationCommand>().ReverseMap();
            CreateMap<CustomerDepositProductPublication, UpdateCustomerDepositProductPublicationCommand>().ReverseMap();
            CreateMap<CustomerDepositProductPublication, CustomerDepositProductPublicationMasterView>().ReverseMap();
            CreateMap<CustomerDepositProductPublicationViewModel, CustomerDepositProductPublicationMasterView>().ReverseMap();
            CreateMap<CreateCustomerDepositProductPublicationCommand, CustomerDepositProductPublicationMasterView>().ReverseMap();
            CreateMap<UpdateCustomerDepositProductPublicationCommand, CustomerDepositProductPublicationMasterView>().ReverseMap();




        }
    }

}