using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient = new();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string realm)
        {
            var tokenResponse = await GetTokenAsync(username, password, realm);

            if (tokenResponse != null)
            {
                HttpContext.Session.SetString("AccessToken", tokenResponse.access_token);
                HttpContext.Session.SetString("RefreshToken", tokenResponse.access_token);
                HttpContext.Session.SetString("X-Tenant", realm);
                ViewBag.Error = "Sucesso.";
                return View();
            }
            else
            {
                ViewBag.Error = "Falha na autenticação.";
                return View();
            }
        }

        private async Task<TokenResponse?> GetTokenAsync(string username, string password, string realm)
        {
            var keycloakUrl = $"http://localhost:8083/realms/{realm}/protocol/openid-connect/token";

            var content = new StringContent($"grant_type=password&client_id={realm}-app&username={username}&password={password}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.PostAsync(keycloakUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TokenResponse>(json);
            }
            return null;
        }
    }

    public class TokenResponse
    {
        public string? access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string? refresh_token { get; set; }
        public string? token_type { get; set; }
        public int not_before_policy { get; set; }
        public string? session_state { get; set; }
        public string? scope { get; set; }
    }
}
