using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class ApplicationRoleModel
    {
        public ApplicationRoleViewModel Role { get; set; }
        public List<ApplicationRoleClaimResponse> RoleClaims { get; set; }
    }
    public class ApplicationRoleClaimResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
