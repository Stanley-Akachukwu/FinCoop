using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles
{
    public class MemberProfileMapperProfile : Profile
    {

        public MemberProfileMapperProfile()
        {

            CreateMap<MemberProfile, MemberProfileViewModel>().ReverseMap();
            CreateMap<MemberProfile, CreateMemberProfileCommand>().ReverseMap();
            CreateMap<MemberProfile, UpdateMemberProfileCommand>().ReverseMap();
            CreateMap<MemberProfile, MemberProfileMasterView>().ReverseMap();
            CreateMap<MemberProfileViewModel, MemberProfileMasterView>().ReverseMap();
            CreateMap<CreateMemberProfileCommand, MemberProfileMasterView>().ReverseMap();
            CreateMap<UpdateMemberProfileCommand, MemberProfileMasterView>().ReverseMap();



            CreateMap<ApproveKYCCommand, UpdateApprovalCommand>().ReverseMap();




        }
    }

}
