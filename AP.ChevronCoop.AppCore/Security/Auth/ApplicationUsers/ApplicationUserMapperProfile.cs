using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUsers
{
    public class ApplicationUserMapperProfile : Profile
    {

        public ApplicationUserMapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
            CreateMap<ApplicationUser, CreateApplicationUserCommand>().ReverseMap();
            CreateMap<ApplicationUser, UpdateApplicationUserCommand>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserMasterView>().ReverseMap();
            CreateMap<ApplicationUserViewModel, ApplicationUserMasterView>().ReverseMap();
            CreateMap<CreateApplicationUserCommand, ApplicationUserMasterView>().ReverseMap();
            CreateMap<UpdateApplicationUserCommand, ApplicationUserMasterView>().ReverseMap();

            CreateMap<ApplicationUserViewModel, MemberProfile>().ReverseMap();
            CreateMap<MemberProfileMasterView, UpdateApplicationUserCommand>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUserId_Email))
                .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => src.ApplicationUserId))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUserId_PhoneNumber));

        }
    }
}
