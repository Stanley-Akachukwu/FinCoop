using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Areas.Identity.Pages.Account
{

    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public class RefreshDashBoardModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEntityDataService DataService;
        private readonly IUtilityService _utilityService;
        public RefreshDashBoardModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, IEntityDataService entityDataService, IUtilityService utilityService)
        {
            _signInManager = signInManager;
            _logger = logger;
            DataService = entityDataService;
            _utilityService = utilityService;
        }
        public bool SwitchToCorporate { get; set; }
        public string Token { get; set; }
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var userName = User.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Identity/Account/Logout");
            }
            Token = User.FindFirstValue(ClaimTypes.Hash);
            SwitchAccountCommand Model = new SwitchAccountCommand()
            {
                UserId = User.FindFirstValue(ClaimTypes.Sid),
                SwitchToCorporate = true
            };
            var rsp = await DataService.Login<SwitchAccountCommand, CommandResult<LoginViewModel>>("switchAccount", Model);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
            if (!rsp.IsSuccessStatusCode)
            {

                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Message;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                    if (!string.IsNullOrEmpty(msg))
                        ModelState.AddModelError(string.Empty, msg);
                }
                else if (rspContent?.Message != null)
                {
                    ModelState.AddModelError(string.Empty, rspContent.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                }
                return Page();

            }
            else
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var rspResponse = JsonSerializer.Deserialize<LoginViewModel>(rsp.Content.Response.ToJson());
                var isAdminStaff = await PopulateClaimsAsync(rspResponse);
                _logger.LogInformation("User logged in.");
                return Redirect("/Dashboard");


            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return LocalRedirect("~/Dashboard");

        }
        private async Task<bool> PopulateClaimsAsync(LoginViewModel response)
        {
            var stream = response.Token;
            if (stream == null)
                stream = Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            bool isAdmin = false;
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, tokenS.Claims.First(claim => claim.Type == "email").Value));
            claims.Add(new Claim(ClaimTypes.Name, tokenS.Claims.First(claim => claim.Type == "unique_name").Value));
            claims.Add(new Claim(ClaimTypes.Sid, tokenS.Claims.First(claim => claim.Type == "sid").Value));
            if (!string.IsNullOrEmpty(response.MembershipId))
            {
                claims.Add(new Claim("MemberShipId", response.MembershipId));
            }
            foreach (var permission in response.Claims)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission.Code));
            }
            foreach (var role in response.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            claims.Add(new Claim(ClaimTypes.GroupSid, "Duo"));
            if (!SwitchToCorporate)
            {
                claims.Add(new Claim(ClaimTypes.Uri, "/admin/dashboard"));
                claims.Add(new Claim(ClaimTypes.UserData, "Switch to Staff User Member"));
                isAdmin = true;
            }
            else
            {
                var isCorporativeMember = response.Roles.Where(f => f.Name.ToLower() == "regular" || f.Name.ToLower() == "retiree" || f.Name.ToLower() == "expatriate").ToList();
                if (isCorporativeMember.Any())
                {
                    claims.Add(new Claim(ClaimTypes.Uri, "/admin/dashboard"));
                    claims.Add(new Claim(ClaimTypes.UserData, "Switch to Staff User"));
                    isAdmin = false;
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Uri, "/dashboard"));
                    claims.Add(new Claim(ClaimTypes.UserData, "Switch to Staff User"));
                    isAdmin = true;
                }
            }
            claims.Add(new Claim(ClaimTypes.Hash, response.Token == null ? Token : response.Token));
            claims.Add(new Claim(ClaimTypes.StateOrProvince, Convert.ToString(response.KycStatus)));
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            { };




            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
new ClaimsPrincipal(claimsIdentity), authProperties);
            return isAdmin;


        }

    }
}
