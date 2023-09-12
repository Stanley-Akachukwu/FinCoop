using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepartmentDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepartmentDepositProductPublications
{
    public class DepartmentDepositProductPublicationMapperProfile : Profile
    {

        public DepartmentDepositProductPublicationMapperProfile()
        {

            CreateMap<DepartmentDepositProductPublication, DepartmentDepositProductPublicationViewModel>().ReverseMap();
            CreateMap<DepartmentDepositProductPublication, CreateDepartmentDepositProductPublicationCommand>().ReverseMap();
            CreateMap<DepartmentDepositProductPublication, UpdateDepartmentDepositProductPublicationCommand>().ReverseMap();
            CreateMap<DepartmentDepositProductPublication, DepartmentDepositProductPublicationMasterView>().ReverseMap();
            CreateMap<DepartmentDepositProductPublicationViewModel, DepartmentDepositProductPublicationMasterView>().ReverseMap();
            CreateMap<CreateDepartmentDepositProductPublicationCommand, DepartmentDepositProductPublicationMasterView>().ReverseMap();
            CreateMap<UpdateDepartmentDepositProductPublicationCommand, DepartmentDepositProductPublicationMasterView>().ReverseMap();




        }
    }

}