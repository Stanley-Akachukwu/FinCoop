using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationRoles
{
    public class ApplicationRoleMapperProfile : Profile
    {

        public ApplicationRoleMapperProfile()
        {

            CreateMap<ApplicationRole, ApplicationRoleViewModel>().ReverseMap();
            CreateMap<ApplicationRole, CreateApplicationRoleCommand>().ReverseMap();
            CreateMap<ApplicationRole, UpdateApplicationRoleCommand>().ReverseMap();
            CreateMap<ApplicationRole, ApplicationRoleMasterView>().ReverseMap();
            CreateMap<ApplicationRoleViewModel, ApplicationRoleMasterView>().ReverseMap();
            CreateMap<CreateApplicationRoleCommand, ApplicationRoleMasterView>().ReverseMap();
            CreateMap<UpdateApplicationRoleCommand, ApplicationRoleMasterView>().ReverseMap();




        }
    }

}
