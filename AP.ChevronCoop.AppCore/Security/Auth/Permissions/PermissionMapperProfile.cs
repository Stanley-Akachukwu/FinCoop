using AP.ChevronCoop.AppDomain.Security.Auth.Permissions;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Auth.Permissions
{
    public class PermissionMapperProfile : Profile
    {

        public PermissionMapperProfile()
        {

            CreateMap<Permission, PermissionViewModel>().ReverseMap();
            CreateMap<Permission, CreatePermissionCommand>().ReverseMap();
            CreateMap<Permission, UpdatePermissionCommand>().ReverseMap();
            CreateMap<Permission, PermissionMasterView>().ReverseMap();
            CreateMap<PermissionViewModel, PermissionMasterView>().ReverseMap();
            CreateMap<CreatePermissionCommand, PermissionMasterView>().ReverseMap();
            CreateMap<UpdatePermissionCommand, PermissionMasterView>().ReverseMap();




        }
    }

}
