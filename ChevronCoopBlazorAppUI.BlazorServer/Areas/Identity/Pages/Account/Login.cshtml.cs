using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEntityDataService DataService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUtilityService _utilityService;
        private readonly IClientAuditService _auditLogService;

       
		[Inject]
        NotificationService notificationService { get; set; }
      
        //private readonly ISessionStorageService _sessionStorage;
        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, IEntityDataService entityDataService, IHttpContextAccessor accessor, IUtilityService utilityService, IClientAuditService auditLogService)
        {
            _signInManager = signInManager;
            _logger = logger;
            DataService = entityDataService;
            _accessor = accessor;
            _utilityService = utilityService;
            _auditLogService = auditLogService;
        }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
		public bool isLoading { get; set; } = false;
		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

		

		}
        public async Task OnGetAsync(string returnUrl = null)
        {
		//	isLoading = true;
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
				isLoading = true;
                returnUrl ??= Url.Content("~/");

                if (ModelState.IsValid)
                {
                    LoginCommand Model = new LoginCommand()
                    {
                        Email = Input.Email,
                        Password = Input.Password
                    };
                    var rsp = await DataService.Login<LoginCommand, CommandResult<LoginViewModel>>("Login", Model);
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
						isLoading  = false;
                        return Page();

                    }
                    else
                    {

                        var rspResponse = JsonSerializer.Deserialize<LoginViewModel>(rsp.Content.Response.ToJson());
                        var isAdminStaff = await PopulateClaimsAsync(rspResponse, Model);
                        _logger.LogInformation("User logged in.");
                        if (isAdminStaff)
                            return Redirect("/admin/dashboard");
                        else
                            return Redirect("/Dashboard");


                    }

                }

                // If we got this far, something failed, redisplay form
           
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error occurred, please contact administrator");
            }
			isLoading  = false;
            return Page();

        }
        private async Task<bool> PopulateClaimsAsync(LoginViewModel response, LoginCommand userLoginModel)
        {
            var stream = response.Token;
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
            if (!string.IsNullOrEmpty(response.CustomerId))
            {
                claims.Add(new Claim("CustomerId", response.CustomerId));
            }

            claims.Add(new Claim("IsAdmin", response.IsAdmin.ToString()));
            claims.Add(new Claim("Token", response.Token));

            foreach (var permission in response.Claims)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission.Code));
            }
            foreach (var role in response.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            if (!string.IsNullOrEmpty(response.ProfilePicture))
            {
                claims.Add(new Claim("ProfilePicture", response.ProfilePicture));
            }
            bool IsBothStaffUserAndCooperativeMember = _utilityService.HasDuoRole(response.Roles);
            if (IsBothStaffUserAndCooperativeMember)
            {
                claims.Add(new Claim(ClaimTypes.GroupSid, "Duo"));
                claims.Add(new Claim(ClaimTypes.Uri, "/Dashboard"));
                claims.Add(new Claim(ClaimTypes.UserData, "Switch to Cooperative Member"));
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

            claims.Add(new Claim(ClaimTypes.StateOrProvince, Convert.ToString(response.KycStatus)));
            claims.Add(new Claim(ClaimTypes.Hash, response.Token));
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            { };

            var currentUser = new ClaimsPrincipal(claimsIdentity);
            userLoginModel.Password = "xxxxxxxxxxx";
            var payload = JsonSerializer.Serialize(userLoginModel);
            var action = "Sign In to Chevron Coop application.";
            await _auditLogService.LogAudit(action, $"Login to the system executed by user with email- {userLoginModel.Email}.", "Security", payload, currentUser);

            var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(stream)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, currentUser, authProperties);
            return isAdmin;


        }

    }
}
