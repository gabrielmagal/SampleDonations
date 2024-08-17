using System.Security.Authentication;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using Sample.Service;

namespace Sample.Controllers
{
    public class ReceiveController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly HttpClient _httpClient = new();
        private readonly ReceiveService _receiveService;
        private readonly UserService _userService;

        public ReceiveController(INotyfService notyf, ReceiveService receiveService, UserService userService)
        {
            _notyf = notyf;
            _receiveService = receiveService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _receiveService.GetAll(HttpContext));
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

        public async Task<IActionResult> Create()
        {
            ReceiveDto receiveDto = new();
            receiveDto.Users = await _userService.GetAll(HttpContext);
            return View(receiveDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReceiveDto receiveDto)
        {
            await _receiveService.Create(receiveDto, HttpContext, _notyf);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            var receive = await _receiveService.GetById(id, HttpContext);
            if (receive == null)
            {
                return NotFound();
            }
            return View(receive);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReceiveDto receiveDto)
        {
            await _receiveService.Edit(receiveDto, HttpContext, _notyf);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            var receive = await _receiveService.GetById(id, HttpContext);
            if (receive == null)
            {
                return NotFound();
            }

            await _receiveService.DeleteById(id, HttpContext, _notyf);

            return RedirectToAction("Index");
        }
    }
}
