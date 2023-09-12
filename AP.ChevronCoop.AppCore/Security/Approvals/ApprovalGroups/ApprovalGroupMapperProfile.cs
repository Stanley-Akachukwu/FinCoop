using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroups
{
    public class ApprovalGroupMapperProfile : Profile
    {
        public ApprovalGroupMapperProfile()
        {
            CreateMap<ApprovalGroup, ApprovalGroupViewModel>().ReverseMap();
            CreateMap<ApprovalGroup, CreateApprovalGroupCommand>().ReverseMap();
            CreateMap<ApprovalGroup, UpdateApprovalGroupCommand>().ReverseMap();
            CreateMap<ApprovalGroup, GetApprovalGroupViewModel>().ReverseMap();
            CreateMap<ApprovalGroupMemberViewModel, ApprovalGroupMember>().ReverseMap();

        }

    }
}
