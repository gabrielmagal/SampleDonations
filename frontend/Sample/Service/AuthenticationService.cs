using System.Net.Http.Headers;
using System.Security.Authentication;

namespace Sample.Service
{
    public class AuthenticationService
    {
        public void AddAuthHeaders(HttpClient httpClient, HttpContext httpContext)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AddTokenHeader(httpClient, httpContext));
            httpClient.DefaultRequestHeaders.Add("X-Tenant", AddTenantHeader(httpClient, httpContext));
        }

        public string AddTokenHeader(HttpClient httpClient, HttpContext httpContext)
        {
            return httpContext.Request.Cookies["AccessToken"] ?? throw new AuthenticationException("Não foi inserido um token.");
        }

        public string AddTenantHeader(HttpClient httpClient, HttpContext httpContext)
        {
            return httpContext.Request.Cookies["X-Tenant"] ?? throw new AuthenticationException("Não foi inserido um tenant.");
        }
    }
}
