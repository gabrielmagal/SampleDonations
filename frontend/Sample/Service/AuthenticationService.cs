using System.Net.Http.Headers;
using System.Security.Authentication;

namespace Sample.Service
{
    public class AuthenticationService
    {
        public static void AddAuthHeaders(HttpClient httpClient, HttpContext httpContext)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AddTokenHeader(httpClient, httpContext));
            if (!httpClient.DefaultRequestHeaders.Contains("X-Tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("X-Tenant", AddTenantHeader(httpClient, httpContext));
            }
        }

        public static string AddTokenHeader(HttpClient httpClient, HttpContext httpContext)
        {
            return httpContext.Session.GetString("AccessToken") ?? throw new AuthenticationException("Não foi inserido um token.");
        }

        public static string AddTenantHeader(HttpClient httpClient, HttpContext httpContext)
        {
            return httpContext.Session.GetString("X-Tenant") ?? throw new AuthenticationException("Não foi inserido um tenant.");
        }
    }
}
