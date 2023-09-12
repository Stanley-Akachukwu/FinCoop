using AP.ChevronCoop.AppDomain.Security.AuditTrails;
using AP.ChevronCoop.Entities.Security.AuditTrails;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.AuditTrails;

public class AuditTrailMapperProfile : Profile
{
    public AuditTrailMapperProfile()
    {
        CreateMap<AuditTrail, AuditTrailViewModel>().ReverseMap();
        CreateMap<AuditTrail, CreateAuditTrailCommand>().ReverseMap();
        CreateMap<AuditTrail, AuditTrailMasterView>().ReverseMap();
        CreateMap<AuditTrailViewModel, AuditTrailMasterView>().ReverseMap();
        CreateMap<CreateAuditTrailCommand, AuditTrailMasterView>().ReverseMap();
    }
}