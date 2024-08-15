using System.Net.Http.Headers;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using SampleDonations.Models;

namespace SampleDonations.Controllers
{
    public class ReceiveController : Controller
    {
        private readonly HttpClient httpClient;

        public ReceiveController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                AddAuthHeaders(httpClient, HttpContext);
                var receives = await httpClient.GetFromJsonAsync<List<ReceiveDto>>($"http://localhost:8080/recebimento");
                return View(receives);
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
        public async Task<IActionResult> Create(ReceiveDto receiveDto)
        {
            AddAuthHeaders(httpClient, HttpContext);
            await httpClient.PostAsJsonAsync($"http://localhost:8080/recebimento", receiveDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            AddAuthHeaders(httpClient, HttpContext);

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
            AddAuthHeaders(httpClient, HttpContext);

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

            await httpClient.PutAsJsonAsync($"http://localhost:8080/recebimento/{receiveDto.Id}", receive);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            AddAuthHeaders(httpClient, HttpContext);

            var receive = await httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/recebimento/{id}");
            if (receive == null)
            {
                return NotFound();
            }

            await httpClient.DeleteAsync($"http://localhost:8080/recebimento/{id}");

            return RedirectToAction("Index");
        }

        public void AddAuthHeaders(HttpClient httpClient, HttpContext httpContext)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AddTokenHeader(httpClient, httpContext));
            httpClient.DefaultRequestHeaders.Add("X-Tenant", AddTenantHeader(httpClient, httpContext));
        }

        public string AddTokenHeader(HttpClient httpClient, HttpContext httpContext)
        {
            return httpContext.Session.GetString("AccessToken") ?? throw new AuthenticationException("Não foi inserido um token.");
        }

        public string AddTenantHeader(HttpClient httpClient, HttpContext httpContext)
        {
            return httpContext.Session.GetString("X-Tenant") ?? throw new AuthenticationException("Não foi inserido um tenant.");
        }
    }
}
