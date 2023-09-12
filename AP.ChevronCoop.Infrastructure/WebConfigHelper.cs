using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace AP.ChevronCoop.Infrastructure
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

        //public async Task<string> GetAccessTokenAsync()
        //{
        //    var token = await HttpContext.GetTokenAsync("access_token");
        //    return token;
        //}

        //public async Task<string> GetOrRefreshAccessTokenAsync()
        //{
        //    var token = await HttpContext.GetUserAccessTokenAsync();
        //    return token;
        //}

        //HttpContextAccessor.HttpContext.User.Identity
        public ClaimsPrincipal CurrentUser { get; set; }
        public string LoggedInUserName => CurrentUser?.Identity?.Name;
    }

}
