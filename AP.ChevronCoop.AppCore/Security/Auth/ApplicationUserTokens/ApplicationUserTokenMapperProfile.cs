using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserTokens;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserTokens;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserTokens
{
    public class ApplicationUserTokenMapperProfile : Profile
    {

        public ApplicationUserTokenMapperProfile()
        {

            CreateMap<ApplicationUserToken, ApplicationUserTokenViewModel>().ReverseMap();
            CreateMap<ApplicationUserToken, CreateApplicationUserTokenCommand>().ReverseMap();
            CreateMap<ApplicationUserToken, UpdateApplicationUserTokenCommand>().ReverseMap();
            CreateMap<ApplicationUserToken, ApplicationUserTokenMasterView>().ReverseMap();
            CreateMap<ApplicationUserTokenViewModel, ApplicationUserTokenMasterView>().ReverseMap();
            CreateMap<CreateApplicationUserTokenCommand, ApplicationUserTokenMasterView>().ReverseMap();
            CreateMap<UpdateApplicationUserTokenCommand, ApplicationUserTokenMasterView>().ReverseMap();




        }
    }

}
