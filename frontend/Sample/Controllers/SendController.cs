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
        private AuthenticationService authenticationService = new();

        public SendController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                authenticationService.AddAuthHeaders(httpClient, HttpContext);
                var sends = await httpClient.GetFromJsonAsync<List<SendDto>>($"http://localhost:8080/entrega");
                return View(sends);
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
        public async Task<IActionResult> Create(SendDto sendDto)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);
            var response = await httpClient.PostAsJsonAsync($"http://localhost:8080/entrega", sendDto);
            ToastService.ShowToastHttp(_notyf, "Doação enviada com sucesso!", response);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

            var send = await httpClient.GetFromJsonAsync<SendDto>($"http://localhost:8080/entrega/{id}");
            if (send == null)
            {
                return NotFound();
            }

            return View(send);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SendDto sendDto)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

            var send = await httpClient.GetFromJsonAsync<SendDto>($"http://localhost:8080/entrega/{sendDto.Id}");
            if (send == null)
            {
                return NotFound();
            }

            send.IdProduct = sendDto.IdProduct;
            send.IdUser = sendDto.IdUser;
            send.Quantity = sendDto.Quantity;
            send.DateTimeDonation = sendDto.DateTimeDonation;

            var response = await httpClient.PutAsJsonAsync($"http://localhost:8080/entrega/{sendDto.Id}", send);

            ToastService.ShowToastHttp(_notyf, "Doação alterada com sucesso!", response);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

            var send = await httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/entrega/{id}");
            if (send == null)
            {
                return NotFound();
            }

            var response = await httpClient.DeleteAsync($"http://localhost:8080/entrega/{id}");

            ToastService.ShowToastHttp(_notyf, "Doação removida com sucesso!", response);

            return RedirectToAction("Index");
        }
    }
}
