using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserRoles
{
    public class ApplicationUserRoleMapperProfile : Profile
    {

        public ApplicationUserRoleMapperProfile()
        {

            CreateMap<ApplicationUserRole, ApplicationUserRoleViewModel>().ReverseMap();
            CreateMap<ApplicationUserRole, CreateApplicationUserRoleCommand>().ReverseMap();
            CreateMap<ApplicationUserRole, UpdateApplicationUserRoleCommand>().ReverseMap();
            CreateMap<ApplicationUserRole, ApplicationUserRoleMasterView>().ReverseMap();
            CreateMap<ApplicationUserRoleViewModel, ApplicationUserRoleMasterView>().ReverseMap();
            CreateMap<CreateApplicationUserRoleCommand, ApplicationUserRoleMasterView>().ReverseMap();
            CreateMap<UpdateApplicationUserRoleCommand, ApplicationUserRoleMasterView>().ReverseMap();




        }
    }

}
