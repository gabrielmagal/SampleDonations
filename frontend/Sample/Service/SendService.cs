using AspNetCoreHero.ToastNotification.Abstractions;
using Sample.Models;

namespace Sample.Service
{
    public class SendService
    {
        private readonly HttpClient _httpClient = new();
        private readonly ToastService _toastService;

        public SendService(ToastService toastService)
        {
            _toastService = toastService;
        }

        public async Task Create(SendDto sendDto, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:8080/entrega", sendDto);
            if (response.IsSuccessStatusCode)
            {
                await _toastService.ShowToast(_notyf, "Doação recebida com sucesso!", TypeToastEnum.SUCCESS);
            }
            else
            {
                await _toastService.ShowToastHttpError(_notyf, response);
            }
        }

        public async Task<List<SendDto?>> GetAll(HttpContext httpContext)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            return await _httpClient.GetFromJsonAsync<List<SendDto?>>($"http://localhost:8080/entrega") ?? [];
        }

        public async Task<SendDto?> GetById(long id, HttpContext httpContext)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            return await _httpClient.GetFromJsonAsync<SendDto>($"http://localhost:8080/entrega/{id}");
        }

        public async Task<SendDto?> Edit(SendDto sendDto, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);

            var send = await _httpClient.GetFromJsonAsync<SendDto>($"http://localhost:8080/entrega/{sendDto.Id}");
            if (send == null)
            {
                await _toastService.ShowToast(_notyf, "Não foi possível encontrar a doação para alteração!", TypeToastEnum.ERROR);
                return null;
            }

            send.Receives = sendDto.Receives;
            send.User = sendDto.User;
            send.Quantity = sendDto.Quantity;
            send.DateTimeDonation = sendDto.DateTimeDonation;

            var response = await _httpClient.PutAsJsonAsync($"http://localhost:8080/entrega/{sendDto.Id}", send);

            if (response.IsSuccessStatusCode)
            {
                await _toastService.ShowToast(_notyf, "Doação alterada com sucesso!", TypeToastEnum.SUCCESS);
                return sendDto;
            }
            await _toastService.ShowToastHttpError(_notyf, response);
            return null;
        }

        public async Task DeleteById(long id, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);

            var send = await _httpClient.GetFromJsonAsync<SendDto>($"http://localhost:8080/entrega/{id}");
            if (send == null)
            {
                await _toastService.ShowToast(_notyf, "Não foi possível encontrar a doação para remoção!", TypeToastEnum.ERROR);
            }

            var response = await _httpClient.DeleteAsync($"http://localhost:8080/entrega/{id}");

            if (response.IsSuccessStatusCode)
            {
                await _toastService.ShowToastHttp(_notyf, "Doação removida com sucesso!", response);
            }
            else
            {
                await _toastService.ShowToastHttpError(_notyf, response);
            }
        }
    }
}
