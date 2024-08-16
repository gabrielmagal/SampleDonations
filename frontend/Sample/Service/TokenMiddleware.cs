using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Sample.Controllers;
using Sample.Models;

namespace Sample.Service
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessToken = context.Request.Cookies["AccessToken"];
            var refreshToken = context.Request.Cookies["RefreshToken"];

            if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;

                if (jwtToken != null && jwtToken.ValidTo <= DateTime.UtcNow.AddSeconds(20))
                {
                    Console.WriteLine($"AccessToken: {accessToken}");
                    Console.WriteLine($"RefreshToken: {refreshToken}");
                    Console.WriteLine($"JWT ValidTo: {jwtToken.ValidTo}");
                    Console.WriteLine($"JWT Expired: {jwtToken.ValidTo <= DateTime.UtcNow}");

                    var newTokenResponse = await RefreshTokenAsync(refreshToken, context.Request.Cookies["X-Tenant"]);

                    if (newTokenResponse != null)
                    {
                        context.Response.Cookies.Append("AccessToken", newTokenResponse.access_token, new CookieOptions { HttpOnly = true, Secure = true });
                        context.Response.Cookies.Append("RefreshToken", newTokenResponse.refresh_token, new CookieOptions { HttpOnly = true, Secure = true });
                    }
                    else
                    {
                        context.Response.Redirect("/Auth/Login");
                        return;
                    }
                }
            }

            await _next(context);
        }

        private async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken, string realm)
        {
            var keycloakUrl = $"http://localhost:8083/realms/{realm}/protocol/openid-connect/token";

            using var client = new HttpClient();
            var content = new StringContent($"grant_type=refresh_token&client_id={realm}-app&refresh_token={refreshToken}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(keycloakUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TokenResponseDto>(json);
            }

            return null;
        }
    }
}
