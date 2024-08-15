using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SampleDonations.Controllers
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var accessToken = context.Session.GetString("AccessToken");
                var refreshToken = context.Session.GetString("RefreshToken");

                if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;

                    if (jwtToken != null && jwtToken.ValidTo <= DateTime.UtcNow.AddSeconds(20))
                    {
                        var newTokenResponse = await RefreshTokenAsync(refreshToken, context.Session.GetString("X-Tenant"));

                        if (newTokenResponse != null)
                        {
                            context.Session.SetString("AccessToken", newTokenResponse.access_token);
                            context.Session.SetString("RefreshToken", newTokenResponse.refresh_token);
                        }
                        else
                        {
                            context.Response.Redirect("/Auth/Login");
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }

        private async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string realm)
        {
            var keycloakUrl = $"http://localhost:8083/realms/{realm}/protocol/openid-connect/token";

            using var client = new HttpClient();
            var content = new StringContent($"grant_type=refresh_token&client_id={realm}-app&refresh_token={refreshToken}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(keycloakUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TokenResponse>(json);
            }

            return null;
        }
    }

}
