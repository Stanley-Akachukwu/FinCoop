using ChevronCoop.Web.AppUI.BlazorServer.Services;
using System.Net.Http.Headers;

namespace ChevronCoop.Web.AppUI.BlazorServer.Config
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILoginService _loginService;

        public AuthenticationHeaderHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            //potentially refresh token here if it has expired etc.
            var token = await _loginService.GetUserToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
