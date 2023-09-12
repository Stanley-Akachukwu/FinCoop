using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChevronCoop.Web.AppUI.BlazorServer.Config
{
    public class WebConfigHelper
    {
        public WebConfigHelper(IConfiguration configuration,
        //IWebHostEnvironment environment,
        IHttpContextAccessor _httpContextAccessor)
        {
            Configuration = configuration;
            // Environment = environment;
            httpContextAccessor = _httpContextAccessor;
            CurrentUser = httpContextAccessor?.HttpContext?.User;


        }

        // public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        private readonly IHttpContextAccessor httpContextAccessor;
        public HttpContext HttpContext => httpContextAccessor?.HttpContext;

        //public IHttpContextAccessor HttpContextAccessor => httpContextAccessor;


        //public void Initialize(IHttpContextAccessor _httpContextAccessor)
        //{
        //    httpContextAccessor = _httpContextAccessor;
        //}

        public async Task<string> GetAccessTokenAsync()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return token;
        }

        //public async Task<string> GetOrRefreshAccessTokenAsync()
        //{
        //    var token = await HttpContext.GetUserAccessTokenAsync();
        //    return token;
        //}

        //HttpContextAccessor.HttpContext.User.Identity
        public ClaimsPrincipal CurrentUser { get; set; }
        public string LoggedInUserName => CurrentUser?.Identity?.Name;

        public Claim SolIdClaim => CurrentUser?.Claims.Where(r => r.Type == ConfigKeys.SOL_ID).FirstOrDefault();
        public string UserSolId => SolIdClaim?.Value;

        public Claim TokenClaim => CurrentUser?.Claims.Where(r => r.Type == "access_token").FirstOrDefault();
        public string AccessToken => TokenClaim?.Value;



        public string UserCentralAdminRole => CurrentUser?.Claims.Where(r => r.Type == ConfigKeys.CENTRAL_ADMIN_ROLE).FirstOrDefault()?.Value;

        public Claim EmployeeNameClaim => CurrentUser?.Claims.Where(r => r.Type == ConfigKeys.EMP_FULL_NAME).FirstOrDefault();
        public Claim EmployeeIdClaim => CurrentUser?.Claims.Where(r => r.Type == ConfigKeys.EMPLOYEE_ID).FirstOrDefault();
        public IEnumerable<Claim> UserRoles => CurrentUser?.Claims.Where(r => r.Type == ClaimTypes.Role);
        public bool IsInRole(string role)
        {
            if (CurrentUser == null) return false;
            return CurrentUser.IsInRole(role);
        }


        public bool IsInRoles(params string[] roles)
        {
            if (CurrentUser == null) return false;
            if (roles == null) return false;
            if (!roles.Any()) return false;

            bool inRole = false;

            foreach (var role in roles)
            {
                if (CurrentUser.IsInRole(role))
                {
                    inRole = true;
                    return inRole;
                }
            }

            return inRole;

        }

        //public bool IsInRoles(this ClaimsPrincipal user, params string[] roles)
        //{
        //    if (user == null) return false;


        //    bool inRole = false;

        //    foreach (var role in roles)
        //    {
        //        if (user.IsInRole(role))
        //        {
        //            inRole = true;
        //            return inRole;
        //        }
        //    }

        //    return inRole;

        //}

        //public bool IsEligible_NewRequest
        //{
        //    get
        //    {
        //        bool rsp = false;

        //        rsp = IsInRoles(RoleNames.ROLE_RM, RoleNames.ROLE_RAM, PermissionNames.INITIATOR);
        //        return rsp;
        //    }
        //}

        //public bool HasClaim(string claimName)
        //{
        //    if (CurrentUser == null) return false;
        //    return CurrentUser.HasClaim(claimName);
        //}

        //public static RefitSettings RefitSettings = new RefitSettings
        //{
        //    //ContentSerializer = new SystemTextJsonContentSerializer()
        //    ContentSerializer = new NewtonsoftJsonContentSerializer()
        //};


        public string GetSetting(string key)
        {
            return Configuration.GetValue<string>(key);
        }

        public string API_HOST => GetSetting(ConfigKeys.API_HOST);
        //public string FINACLE_API_HOST => GetSetting(ConfigKeys.FINACLE_API_HOST);
        public string ODATA_VIEWS_HOST => GetSetting(ConfigKeys.ODATA_VIEWS_HOST);
        public string APP_NAME => GetSetting(ConfigKeys.APP_NAME);
        public string APP_WEB_URL => GetSetting(ConfigKeys.APP_WEB_URL);


    }






}
