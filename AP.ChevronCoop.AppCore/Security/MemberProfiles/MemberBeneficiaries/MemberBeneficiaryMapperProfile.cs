using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBeneficiaries;

public class MemberBeneficiaryMapperProfile : Profile
{
    public MemberBeneficiaryMapperProfile()
    {
        CreateMap<MemberBeneficiary, MemberBeneficiaryViewModel>().ReverseMap();
        CreateMap<MemberBeneficiary, CreateMemberBeneficiaryCommand>().ReverseMap();
        CreateMap<MemberBeneficiary, UpdateMemberBeneficiaryCommand>().ReverseMap();
        CreateMap<MemberBeneficiary, MemberBeneficiaryMasterView>().ReverseMap();
        CreateMap<MemberBeneficiaryViewModel, MemberBeneficiaryMasterView>().ReverseMap();
        CreateMap<CreateMemberBeneficiaryCommand, MemberBeneficiaryMasterView>().ReverseMap();
        CreateMap<UpdateMemberBeneficiaryCommand, MemberBeneficiaryMasterView>().ReverseMap();
    }
}