using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalRoles;
using AP.ChevronCoop.Entities.Security.Approvals;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalRoles
{
    public class ApprovalRoleMapperProfile : Profile
    {

        public ApprovalRoleMapperProfile()
        {

            CreateMap<ApprovalRole, ApprovalRoleViewModel>().ReverseMap();
            CreateMap<ApprovalRole, CreateApprovalRoleCommand>().ReverseMap();
            CreateMap<ApprovalRole, UpdateApprovalRoleCommand>().ReverseMap();
            CreateMap<ApprovalRole, ApprovalRoleMasterView>().ReverseMap();
            CreateMap<ApprovalRoleViewModel, ApprovalRoleMasterView>().ReverseMap();
            CreateMap<CreateApprovalRoleCommand, ApprovalRoleMasterView>().ReverseMap();
            CreateMap<UpdateApprovalRoleCommand, ApprovalRoleMasterView>().ReverseMap();




        }
    }

}
