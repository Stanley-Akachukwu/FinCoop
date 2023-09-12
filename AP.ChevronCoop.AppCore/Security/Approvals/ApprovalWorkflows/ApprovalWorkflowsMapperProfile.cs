using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalWorkflows
{
    public class ApprovalWorkflowsMapperProfile : Profile
    {
        public ApprovalWorkflowsMapperProfile()
        {
            CreateMap<ApprovalWorkflow, ApprovalWorkflowViewModel>().ReverseMap();
            CreateMap<ApprovalWorkflow, CreateApprovalWorkflowCommand>()
                .ForMember(c => c.ApprovalGroups, option => option.Ignore()).ReverseMap();
            CreateMap<ApprovalWorkflow, UpdateApprovalWorkflowCommand>().ReverseMap();
            CreateMap<ApprovalWorkflow, ApprovalWorkflowMasterView>().ReverseMap();
            CreateMap<ApprovalWorkflowViewModel, ApprovalWorkflowMasterView>().ReverseMap();
            CreateMap<CreateApprovalCommand, ApprovalWorkflowMasterView>().ReverseMap();
            CreateMap<UpdateApprovalCommand, ApprovalWorkflowMasterView>().ReverseMap();
        }

    }
}
