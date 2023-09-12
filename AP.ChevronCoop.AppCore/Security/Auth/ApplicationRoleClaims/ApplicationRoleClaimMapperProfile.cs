using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationRoleClaims
{
    public class ApplicationRoleClaimMapperProfile : Profile
    {

        public ApplicationRoleClaimMapperProfile()
        {

            CreateMap<ApplicationRoleClaim, ApplicationRoleClaimViewModel>().ReverseMap();
            CreateMap<ApplicationRoleClaim, CreateApplicationRoleClaimCommand>().ReverseMap();
            CreateMap<ApplicationRoleClaim, UpdateApplicationRoleClaimCommand>().ReverseMap();
            CreateMap<ApplicationRoleClaim, ApplicationRoleClaimMasterView>().ReverseMap();
            CreateMap<ApplicationRoleClaimViewModel, ApplicationRoleClaimMasterView>().ReverseMap();
            CreateMap<CreateApplicationRoleClaimCommand, ApplicationRoleClaimMasterView>().ReverseMap();
            CreateMap<UpdateApplicationRoleClaimCommand, ApplicationRoleClaimMasterView>().ReverseMap();




        }
    }

}
