using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using Microsoft.IdentityModel.Tokens;

namespace ChevronCoop.API.Config;

public static class TokenManager
{
    /// <summary>
    /// Generate token for logged in user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="Config"></param>
    /// <returns></returns>
    public static LoginViewModel GenerateToken(this LoginViewModel user, IConfiguration Config)
    {
        if (Config != null && user != null && user.Email is not null)
        {
            // Create Claims
            var UserClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                // new Claim("MId", user.MembershipId),
            };

            var secretBytes = Encoding.UTF8.GetBytes(Config["Jwt:Key"]);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;
            var signingcredentials = new SigningCredentials(key, algorithm);
            var token = new JwtSecurityToken(
                issuer: Config["Jwt:Issuer"],
                audience: Config["Jwt:Issuer"],
                claims: UserClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingcredentials);

            user.Token = new JwtSecurityTokenHandler().WriteToken(token);
        }

        return user;
    }

    /// <summary>
    /// Deserialize token
    /// </summary>
    /// <param name="userClaims"></param>
    /// <returns></returns>
    public static ApplicationUser GetTokenInfo(ClaimsIdentity userClaims)
    {
        ApplicationUser user = null;
        if (userClaims != null)
        {
            user = new ApplicationUser();
            IList<Claim> claim = userClaims.Claims.ToList();
            user.Id = claim[0].Value;
            user.UserName = claim[1].Value;
            user.Email = claim[2].Value;
        }

        return user;
    }
}