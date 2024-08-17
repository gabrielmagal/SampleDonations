using System.Security.Authentication;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using Sample.Service;

namespace Sample.Controllers
{
    public class SendController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly HttpClient httpClient = new();
        private readonly ReceiveService _receiveService;
        private readonly UserService _userService;
        private readonly SendService _sendService;

        public SendController(INotyfService notyf, ReceiveService receiveService, UserService userService, SendService sendService)
        {
            _notyf = notyf;
            _receiveService = receiveService;
            _userService = userService;
            _sendService = sendService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _sendService.GetAll(HttpContext));
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
            SendDto sendDto = new()
            {
                Users = await _userService.GetAll(HttpContext),
                Receives = await _receiveService.GetAll(HttpContext)
            };
            return View(sendDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SendDto sendDto)
        {
            await _sendService.Create(sendDto, HttpContext, _notyf);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            var send = await _sendService.GetById(id, HttpContext);
            if (send == null)
            {
                return NotFound();
            }
            return View(send);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SendDto sendDto)
        {
            await _sendService.Edit(sendDto, HttpContext, _notyf);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            var send = await _sendService.GetById(id, HttpContext);
            if (send == null)
            {
                return NotFound();
            }

            await _sendService.DeleteById(id, HttpContext, _notyf);

            return RedirectToAction("Index");
        }
    }
}
