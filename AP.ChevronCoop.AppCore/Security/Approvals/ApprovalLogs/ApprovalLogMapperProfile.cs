using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalLogs;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalLogs;

public class ApprovalLogMapperProfile : Profile
{
  public ApprovalLogMapperProfile()
  {
    CreateMap<ApprovalLog, ApprovalLogViewModel>().ReverseMap();
    CreateMap<ApprovalLog, CreateApprovalLogCommand>().ReverseMap();
    CreateMap<ApprovalLog, UpdateApprovalLogCommand>().ReverseMap();
    CreateMap<ApprovalLog, ApprovalLogMasterView>().ReverseMap();
    CreateMap<ApprovalLogViewModel, ApprovalLogMasterView>().ReverseMap();
    CreateMap<CreateApprovalLogCommand, ApprovalLogMasterView>().ReverseMap();
    CreateMap<UpdateApprovalLogCommand, ApprovalLogMasterView>().ReverseMap();
  }
}