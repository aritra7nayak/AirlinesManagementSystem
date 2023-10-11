using AirlineManagementSystem.DTOs;

namespace AirlineManagementSystem.Services
{
    public class UserRegistrationLogin
    {
        private readonly HttpClient _httpClient;

        public UserRegistrationLogin(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> RegisterAsync(RegisterViewModel model)
        {
            return await _httpClient.PostAsJsonAsync("api/AccountApi/register", model);
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginViewModel model)
        {
            return await _httpClient.PostAsJsonAsync("api/AccountApi/login", model);
        }
    }
}
