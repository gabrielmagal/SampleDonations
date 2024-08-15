using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using SampleDonations.Models;
using SampleDonations.Service;

namespace SampleDonations.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient httpClient;
        private AuthService authService = new();

        public UserController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                authService.AddAuthHeaders(httpClient, HttpContext);
                var users = await httpClient.GetFromJsonAsync<List<UserDto>>($"http://localhost:8080/usuario");
                return View(users);
            }
            catch (AuthenticationException)
            {
                return RedirectPermanent("/Auth/Login");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            authService.AddAuthHeaders(httpClient, HttpContext);
            await httpClient.PostAsJsonAsync($"http://localhost:8080/usuario", userDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            authService.AddAuthHeaders(httpClient, HttpContext);

            var user = await httpClient.GetFromJsonAsync<UserDto>($"http://localhost:8080/usuario/{id}");
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDto userDto)
        {
            authService.AddAuthHeaders(httpClient, HttpContext);

            var user = await httpClient.GetFromJsonAsync<UserDto>($"http://localhost:8080/usuario/{userDto.Id}");
            if (user == null)
            {
                return NotFound();
            }

            user.Name = userDto.Name;
            user.Password = userDto.Password;
            user.Email = userDto.Email;
            user.Cpf = userDto.Cpf;
            user.Photo = userDto.Photo;
            user.DateOfBirth = userDto.DateOfBirth;
            user.UserPermissionType = userDto.UserPermissionType;

            await httpClient.PutAsJsonAsync($"http://localhost:8080/recebimento/{userDto.Id}", user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            authService.AddAuthHeaders(httpClient, HttpContext);

            var user = await httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/usuario/{id}");
            if (user == null)
            {
                return NotFound();
            }

            await httpClient.DeleteAsync($"http://localhost:8080/usuario/{id}");

            return RedirectToAction("Index");
        }
    }
}
