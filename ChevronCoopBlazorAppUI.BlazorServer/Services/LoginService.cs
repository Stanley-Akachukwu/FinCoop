namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
    public class LoginService : ILoginService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetUserToken()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                if (context != null)
                {
                    var Principal = context.User.Identity.Name;
                    if (!string.IsNullOrEmpty(Principal))
                    {
                        var user = _httpContextAccessor.HttpContext.User;

                        if (user.Identity.IsAuthenticated)
                        {
                            var tokenClaim = user.Claims.Where(f => f.Type == "Token").FirstOrDefault();
                            if (tokenClaim != null)
                                return tokenClaim.Value;
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                return null;
            }

            return string.Empty;

        }
    }
}
