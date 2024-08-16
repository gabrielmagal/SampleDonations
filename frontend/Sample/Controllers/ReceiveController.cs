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
        private readonly HttpClient httpClient = new();
        private AuthenticationService authenticationService = new();

        public ReceiveController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                authenticationService.AddAuthHeaders(httpClient, HttpContext);
                var receives = await httpClient.GetFromJsonAsync<List<ReceiveDto>>($"http://localhost:8080/recebimento");
                return View(receives);
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
        public async Task<IActionResult> Create(ReceiveDto receiveDto)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);
            var response = await httpClient.PostAsJsonAsync($"http://localhost:8080/recebimento", receiveDto);
            ToastService.ShowToastHttp(_notyf, "Doação recebida com sucesso!", response);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

            var receive = await httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/recebimento/{id}");
            if (receive == null)
            {
                return NotFound();
            }

            return View(receive);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReceiveDto receiveDto)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

            var receive = await httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/recebimento/{receiveDto.Id}");
            if (receive == null)
            {
                return NotFound();
            }

            receive.Name = receiveDto.Name;
            receive.TypeOfDonation = receiveDto.TypeOfDonation;
            receive.Quantity = receiveDto.Quantity;
            receive.Validity = receiveDto.Validity;
            receive.DateTimeReceipt = receiveDto.DateTimeReceipt;

            var response = await httpClient.PutAsJsonAsync($"http://localhost:8080/recebimento/{receiveDto.Id}", receive);

            ToastService.ShowToastHttp(_notyf, "Doação alterada com sucesso!", response);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            authenticationService.AddAuthHeaders(httpClient, HttpContext);

            var receive = await httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/recebimento/{id}");
            if (receive == null)
            {
                return NotFound();
            }

            var response = await httpClient.DeleteAsync($"http://localhost:8080/recebimento/{id}");

            ToastService.ShowToastHttp(_notyf, "Doação removida com sucesso!", response);

            return RedirectToAction("Index");
        }
    }
}
