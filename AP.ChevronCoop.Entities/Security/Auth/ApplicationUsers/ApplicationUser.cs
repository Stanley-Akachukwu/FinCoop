using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserTokens;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Microsoft.AspNetCore.Identity;

namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

//Login profile table
public class ApplicationUser : IdentityUser<string>
{
  public string? AdObjectId { get; set; }
  public bool IsAdmin { get; set; } = false;
  public string? SecondaryPhone { get; set; }
  public bool SecondaryPhoneConfirmed { get; set; }
  
  public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
  public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
  public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
  public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
  public virtual MemberProfile MemberProfile { get; set; }
}