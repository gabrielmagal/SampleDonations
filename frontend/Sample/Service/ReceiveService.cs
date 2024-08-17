using AspNetCoreHero.ToastNotification.Abstractions;
using Sample.Models;

namespace Sample.Service
{
    public class ReceiveService
    {
        private readonly HttpClient _httpClient = new();
        private readonly ToastService _toastService;

        public ReceiveService(ToastService toastService)
        {
            _toastService = toastService;
        }

        public async Task Create(ReceiveDto receiveDto, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:8080/recebimento", receiveDto);
            if (response.IsSuccessStatusCode)
            {
                await _toastService.ShowToast(_notyf, "Doação recebida com sucesso!", TypeToastEnum.SUCCESS);
            }
            else
            {
                await _toastService.ShowToastHttpError(_notyf, response);
            }
        }

        public async Task<List<ReceiveDto?>> GetAll(HttpContext httpContext)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            return await _httpClient.GetFromJsonAsync<List<ReceiveDto?>>($"http://localhost:8080/recebimento") ?? [];
        }

        public async Task<ReceiveDto?> GetById(long id, HttpContext httpContext)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            return await _httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/recebimento/{id}");
        }

        public async Task<ReceiveDto?> Edit(ReceiveDto receiveDto, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);

            var receive = await _httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/recebimento/{receiveDto.Id}");
            if (receive == null)
            {
                await _toastService.ShowToast(_notyf, "Não foi possível encontrar a doação para alteração!", TypeToastEnum.ERROR);
                return null;
            }

            receive.Name = receiveDto.Name;
            receive.TypeOfDonationEnum = receiveDto.TypeOfDonationEnum;
            receive.Quantity = receiveDto.Quantity;
            receive.Validity = receiveDto.Validity;
            receive.DateTimeReceipt = receiveDto.DateTimeReceipt;

            var response = await _httpClient.PutAsJsonAsync($"http://localhost:8080/recebimento/{receiveDto.Id}", receive);

            if (response.IsSuccessStatusCode)
            {
                await _toastService.ShowToast(_notyf, "Doação alterada com sucesso!", TypeToastEnum.SUCCESS);
                return receiveDto;
            }
            await _toastService.ShowToastHttpError(_notyf, response);
            return null;
        }

        public async Task DeleteById(long id, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);

            var receive = await _httpClient.GetFromJsonAsync<ReceiveDto>($"http://localhost:8080/recebimento/{id}");
            if (receive == null)
            {
                await _toastService.ShowToast(_notyf, "Não foi possível encontrar a doação para remoção!", TypeToastEnum.ERROR);
            }

            var response = await _httpClient.DeleteAsync($"http://localhost:8080/recebimento/{id}");

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
