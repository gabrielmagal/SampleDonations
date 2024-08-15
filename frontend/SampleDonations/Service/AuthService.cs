using System.Net.Http.Headers;
using System.Security.Authentication;

namespace SampleDonations.Service
{
    public class AuthService
    {
        public void AddAuthHeaders(HttpClient httpClient, HttpContext httpContext)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AddTokenHeader(httpClient, httpContext));
            httpClient.DefaultRequestHeaders.Add("X-Tenant", AddTenantHeader(httpClient, httpContext));
        }

        public string AddTokenHeader(HttpClient httpClient, HttpContext httpContext)
        {
            return httpContext.Session.GetString("AccessToken") ?? throw new AuthenticationException("Não foi inserido um token.");
        }

        public string AddTenantHeader(HttpClient httpClient, HttpContext httpContext)
        {
            return httpContext.Session.GetString("X-Tenant") ?? throw new AuthenticationException("Não foi inserido um tenant.");
        }
    }
}
