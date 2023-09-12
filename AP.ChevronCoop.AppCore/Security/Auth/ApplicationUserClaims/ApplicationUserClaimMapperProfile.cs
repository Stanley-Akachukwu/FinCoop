using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserClaims;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserClaims
{
    public class ApplicationUserClaimMapperProfile : Profile
    {

        public ApplicationUserClaimMapperProfile()
        {

            CreateMap<ApplicationUserClaim, ApplicationUserClaimViewModel>().ReverseMap();
            CreateMap<ApplicationUserClaim, CreateApplicationUserClaimCommand>().ReverseMap();
            CreateMap<ApplicationUserClaim, UpdateApplicationUserClaimCommand>().ReverseMap();
            CreateMap<ApplicationUserClaim, ApplicationUserClaimMasterView>().ReverseMap();
            CreateMap<ApplicationUserClaimViewModel, ApplicationUserClaimMasterView>().ReverseMap();
            CreateMap<CreateApplicationUserClaimCommand, ApplicationUserClaimMasterView>().ReverseMap();
            CreateMap<UpdateApplicationUserClaimCommand, ApplicationUserClaimMasterView>().ReverseMap();




        }
    }

}
