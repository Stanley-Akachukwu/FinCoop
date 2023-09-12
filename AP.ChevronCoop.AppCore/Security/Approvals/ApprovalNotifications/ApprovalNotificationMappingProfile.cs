using AutoMapper;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalNotifications;

public class ApprovalNotificationMappingProfile : Profile
{
    public ApprovalNotificationMappingProfile()
    {
        CreateMap<ApprovalNotification, CreateApprovalNotificationCommand>().ReverseMap();
    }
}