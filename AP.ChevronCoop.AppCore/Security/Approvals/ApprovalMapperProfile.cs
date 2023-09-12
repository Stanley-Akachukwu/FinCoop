using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Approvals;

public class ApprovalMapperProfile : Profile
{
    public ApprovalMapperProfile()
    {
        CreateMap<Approval, ApprovalViewModel>().ReverseMap();
        CreateMap<Approval, CreateApprovalCommand>().ReverseMap();
        CreateMap<Approval, UpdateApprovalCommand>().ReverseMap();
        CreateMap<Approval, CreateApprovalModel>().ReverseMap();
    }
}