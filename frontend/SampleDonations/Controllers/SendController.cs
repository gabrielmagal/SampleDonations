using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using SampleDonations.Models;
using SampleDonations.Service;

namespace SampleDonations.Controllers
{
    public class SendController : Controller
    {
        private readonly HttpClient httpClient;
        private AuthService authService = new();

        public SendController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                authService.AddAuthHeaders(httpClient, HttpContext);
                var sends = await httpClient.GetFromJsonAsync<List<SendDto>>($"http://localhost:8080/entrega");
                return View(sends);
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
        public async Task<IActionResult> Create(SendDto sendDto)
        {
            authService.AddAuthHeaders(httpClient, HttpContext);
            await httpClient.PostAsJsonAsync($"http://localhost:8080/entrega", sendDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            authService.AddAuthHeaders(httpClient, HttpContext);

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
            authService.AddAuthHeaders(httpClient, HttpContext);

            var send = await httpClient.GetFromJsonAsync<SendDto>($"http://localhost:8080/entrega/{sendDto.Id}");
            if (send == null)
            {
                return NotFound();
            }

            send.IdProduct = sendDto.IdProduct;
            send.IdUser = sendDto.IdUser;
            send.Quantity = sendDto.Quantity;
            send.DateTimeDonation = sendDto.DateTimeDonation;

            await httpClient.PutAsJsonAsync($"http://localhost:8080/entrega/{sendDto.Id}", send);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            authService.AddAuthHeaders(httpClient, HttpContext);

            var send = await httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/entrega/{id}");
            if (send == null)
            {
                return NotFound();
            }

            await httpClient.DeleteAsync($"http://localhost:8080/entrega/{id}");

            return RedirectToAction("Index");
        }
    }
}
