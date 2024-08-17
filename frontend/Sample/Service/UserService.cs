using AspNetCoreHero.ToastNotification.Abstractions;
using Sample.Models;

namespace Sample.Service
{
    public class UserService
    {
        private readonly HttpClient _httpClient = new();
        private readonly ToastService _toastService;

        public UserService(ToastService toastService)
        {
            _toastService = toastService;
        }

        public async Task Create(UserDto userDto, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:8080/usuario", userDto);
            if (response.IsSuccessStatusCode)
            {
                await _toastService.ShowToast(_notyf, "Usuário criado com sucesso!", TypeToastEnum.SUCCESS);
            }
            else
            {
                await _toastService.ShowToastHttpError(_notyf, response);
            }
        }

        public async Task<List<UserDto?>> GetAll(HttpContext httpContext)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            return await _httpClient.GetFromJsonAsync<List<UserDto?>>($"http://localhost:8080/usuario") ?? [];
        }

        public async Task<UserDto?> GetById(long id, HttpContext httpContext)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);
            return await _httpClient.GetFromJsonAsync<UserDto>($"http://localhost:8080/usuario/{id}");
        }

        public async Task<UserDto?> Edit(UserDto userDto, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);

            var user = await _httpClient.GetFromJsonAsync<UserDto>($"http://localhost:8080/usuario/{userDto.Id}");
            if (user == null)
            {
                await _toastService.ShowToast(_notyf, "Não foi possível encontrar o usuário para alteração!", TypeToastEnum.ERROR);
                return null;
            }

            user.Name = userDto.Name;
            user.Password = userDto.Password;
            user.Email = userDto.Email;
            user.Cpf = userDto.Cpf;
            user.Photo = userDto.Photo;
            user.DateOfBirth = userDto.DateOfBirth;
            user.UserPermissionTypeEnum = userDto.UserPermissionTypeEnum;

            var response = await _httpClient.PutAsJsonAsync($"http://localhost:8080/usuario/{userDto.Id}", user);

            if (response.IsSuccessStatusCode)
            {
                await _toastService.ShowToast(_notyf, "Usuário alterado com sucesso!", TypeToastEnum.SUCCESS);
                return user;
            }
            await _toastService.ShowToastHttpError(_notyf, response);
            return null;
        }

        public async Task DeleteById(long id, HttpContext httpContext, INotyfService _notyf)
        {
            AuthenticationService.AddAuthHeaders(_httpClient, httpContext);

            var user = await _httpClient.GetFromJsonAsync<UserDto>($"http://localhost:8080/usuario/{id}");
            if (user == null)
            {
                await _toastService.ShowToast(_notyf, "Não foi possível encontrar o usuário para remoção!", TypeToastEnum.ERROR);
            }

            var response = await _httpClient.DeleteAsync($"http://localhost:8080/usuario/{id}");

            if (response.IsSuccessStatusCode)
            {
                await _toastService.ShowToastHttp(_notyf, "Usuário removido com sucesso!", response);
            }
            else
            {
                await _toastService.ShowToastHttpError(_notyf, response);
            }
        }
    }
}
