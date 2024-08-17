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
        private readonly HttpClient _httpClient = new();
        private readonly UserService _userService;

        public UserController(INotyfService notyf, UserService userService)
        {
            _notyf = notyf;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _userService.GetAll(HttpContext));
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
            await _userService.Create(userDto, HttpContext, _notyf);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            var user = await _userService.GetById(id, HttpContext);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDto userDto)
        {
            await _userService.Edit(userDto, HttpContext, _notyf);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            var user = await _userService.GetById(id, HttpContext);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteById(id, HttpContext, _notyf);
            return RedirectToAction("Index");
        }
    }
}
