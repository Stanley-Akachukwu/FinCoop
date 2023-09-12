using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroupMembers
{
    public class ApprovalGroupMemberMapperProfile : Profile
    {
        public ApprovalGroupMemberMapperProfile()
        {
            CreateMap<ApprovalGroupMember, CreateOrUpdateGroupMemberCommand>().ReverseMap();
            CreateMap<ApprovalGroupMember, CreateApprovalGroupMemberCommand>().ReverseMap();
        }

    }
}
