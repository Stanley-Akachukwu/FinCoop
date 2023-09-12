namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;

public class LoginViewModel
{
  public string Id { get; set; }
  public string Email { get; set; }
  public string Token { get; set; }
  public string LastName { get; set; }
  public string MiddleName { get; set; }
  public string FirstName { get; set; }
  public string MembershipId { get; set; }
  public string MembershipType { get; set; }
  public string ProfilePicture { get; set; }
  public bool IsAdmin { get; set; }
  public string CustomerId { get; set; }
  public bool KycStatus { get; set; }
  public List<RoleLookup> Roles { get; set; }
  public List<LoginClaim> Claims { get; set; }
}

public class LoginClaim
{
  public string Id { get; set; }
  public string Code { get; set; }
}

public class RoleLookup
{
  public string Id { get; set; }
  public string Code { get; set; }
  public string Name { get; set; }
}