using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using Microsoft.AspNetCore.Identity;

namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationUserTokens
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {

        public ApplicationUserToken()
        {
            //Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString();
        }
        public virtual ApplicationUser User { get; set; }
    }
}
