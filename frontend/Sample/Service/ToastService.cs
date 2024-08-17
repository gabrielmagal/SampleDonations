using System.Text.Json;
using AspNetCoreHero.ToastNotification.Abstractions;
using Sample.Models;

namespace Sample.Service
{
    public class ToastService
    {
        public async Task ShowToast(INotyfService _notyf, string message, TypeToastEnum typeToastEnum, int time = 8)
        {
            switch (typeToastEnum)
            {
                case TypeToastEnum.SUCCESS:
                    _notyf.Success(message, time);
                    break;

                case TypeToastEnum.ERROR:
                    _notyf.Error(message, time);
                    break;

                case TypeToastEnum.WARNING:
                    _notyf.Warning(message, time);
                    break;

                case TypeToastEnum.INFORMATION:
                    _notyf.Information(message, time);
                    break;

                case TypeToastEnum.CUSTOM1:
                    _notyf.Custom(message, time, "whitesmoke", "fa fa-gear");
                    break;

                case TypeToastEnum.CUSTOM2:
                    _notyf.Custom(message, time, "#0c343d", "fa fa-user");
                    break;
            }
        }

        public async Task ShowToastHttpError(INotyfService _notyf, HttpResponseMessage _httpResponseMessage)
        {
            string result = _httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (result.Contains("Constraint Violation"))
            {
                var errorResponse = JsonSerializer.Deserialize<ConstraintViolationDto>(result);
                await ShowToast(_notyf, errorResponse?.violations[0]?.message ?? "Não foi possível realizar a operação.", TypeToastEnum.ERROR);
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(result);
                var errorMessage = errorResponse?.details?.Split(":").LastOrDefault()?.Trim();
                await ShowToast(_notyf, errorMessage ?? "Não foi possível realizar a operação.", TypeToastEnum.ERROR);
            }
        }

        public async Task ShowToastHttp(INotyfService _notyf, string messageSuccess, HttpResponseMessage _httpResponseMessage)
        {
            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                await ShowToast(_notyf, messageSuccess, TypeToastEnum.SUCCESS);
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(_httpResponseMessage.Content.ReadAsStringAsync().Result);
                var errorMessage = errorResponse?.details?.Split(":").LastOrDefault()?.Trim();
                await ShowToast(_notyf, errorMessage ?? "Não foi possível realizar a operação.", TypeToastEnum.ERROR);
            }
        }
    }
}
