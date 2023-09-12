using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberNextOfKins;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberNextOfKins;

public class MemberNextOfKinMapperProfile : Profile
{
    public MemberNextOfKinMapperProfile()
    {
        CreateMap<MemberNextOfKin, MemberNextOfKinViewModel>().ReverseMap();
        CreateMap<MemberNextOfKin, CreateMemberNextOfKinCommand>().ReverseMap();
        CreateMap<MemberNextOfKin, UpdateMemberNextOfKinCommand>().ReverseMap();
        CreateMap<MemberNextOfKin, MemberNextOfKinMasterView>().ReverseMap();
        CreateMap<MemberNextOfKinViewModel, MemberNextOfKinMasterView>().ReverseMap();
        CreateMap<CreateMemberNextOfKinCommand, MemberNextOfKinMasterView>().ReverseMap();
        CreateMap<UpdateMemberNextOfKinCommand, MemberNextOfKinMasterView>().ReverseMap();
    }
}