using System.Security.Authentication;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using Sample.Service;

namespace Sample.Controllers
{
    public class UserController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly HttpClient httpClient = new();
        private AuthenticationService authenticationService = new();

        public UserController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                authenticationService.AddAuthHeaders(httpClient, HttpContext);
                var users = await httpClient.GetFromJsonAsync<List<UserDto>>($"http://localhost:8080/usuario");
                return View(users);
            }
            catch (AuthenticationException)
            {
                return RedirectToAction("Login", "Auth");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);
            var response = await httpClient.PostAsJsonAsync($"http://localhost:8080/usuario", userDto);
            ToastService.ShowToastHttp(_notyf, "Usuário criado com sucesso!", response);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

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
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

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

            var response = await httpClient.PutAsJsonAsync($"http://localhost:8080/recebimento/{userDto.Id}", user);

            ToastService.ShowToastHttp(_notyf, "Usuário alterado com sucesso!", response);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

            var user = await httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/usuario/{id}");
            if (user == null)
            {
                return NotFound();
            }

            var response = await httpClient.DeleteAsync($"http://localhost:8080/usuario/{id}");

            ToastService.ShowToastHttp(_notyf, "Usuário removido com sucesso!", response);

            return RedirectToAction("Index");
        }
    }
}
