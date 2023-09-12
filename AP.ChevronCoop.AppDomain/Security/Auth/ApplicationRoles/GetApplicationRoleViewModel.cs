namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;

public class GetApplicationRoleViewModel
{
  public ApplicationRoleViewModel Role { get; set; }
  public List<ApplicationRoleClaimResponse> RoleClaims { get; set; }
}

public class ApplicationRoleClaimResponse
{
  public string Id { get; set; }
  public string Name { get; set; }
}