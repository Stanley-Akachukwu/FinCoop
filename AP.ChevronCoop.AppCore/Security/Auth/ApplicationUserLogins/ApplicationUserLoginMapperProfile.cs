using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{
    public class ApplicationUserLoginMapperProfile : Profile
    {

        public ApplicationUserLoginMapperProfile()
        {

            CreateMap<ApplicationUserLogin, ApplicationUserLoginViewModel>().ReverseMap();
            CreateMap<ApplicationUserLogin, CreateApplicationUserLoginCommand>().ReverseMap();
            CreateMap<ApplicationUserLogin, UpdateApplicationUserLoginCommand>().ReverseMap();
            CreateMap<ApplicationUserLogin, ApplicationUserLoginMasterView>().ReverseMap();
            CreateMap<ApplicationUserLoginViewModel, ApplicationUserLoginMasterView>().ReverseMap();
            CreateMap<CreateApplicationUserLoginCommand, ApplicationUserLoginMasterView>().ReverseMap();
            CreateMap<UpdateApplicationUserLoginCommand, ApplicationUserLoginMasterView>().ReverseMap();




        }
    }

}
