//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ChevronCoop.Web.AppUI.BlazorServer.Config
//{
//    public class RefitDelegatingHandler : DelegatingHandler
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly ILogger<RefitDelegatingHandler> _logger;

//        public RefitDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<RefitDelegatingHandler> logger)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _logger = logger;
//        }

//        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            HttpResponseMessage httpResponseMessage;

//            try
//            {
//                string accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
//                //string accessToken = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync();

//                if (string.IsNullOrEmpty(accessToken))
//                {
//                    throw new Exception($"Access token is missing for the request {request.RequestUri}");
//                }

//                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

//                var headers = _httpContextAccessor.HttpContext.Request.Headers;
//                if (headers.ContainsKey("X-Correlation-ID") && !string.IsNullOrEmpty(headers["X-Correlation-ID"]))
//                {
//                    request.Headers.Add("X-Correlation-ID", headers["X-Correlation-ID"].ToString());
//                }

//                httpResponseMessage = await base.SendAsync(request, cancellationToken);
//                httpResponseMessage.EnsureSuccessStatusCode();
//            }
//            catch (Exception ex)
//            {
//               // _logger.LogError(ex, "Failed to run http query {RequestUri}", request.RequestUri);
//                throw;
//            }
//            return httpResponseMessage;
//        }
//    }

//}
