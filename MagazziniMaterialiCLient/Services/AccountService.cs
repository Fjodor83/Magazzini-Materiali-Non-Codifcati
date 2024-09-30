using MagazziniMaterialiCLient.Models;
using MagazziniMaterialiCLient.Models.Entity.DTOs;
using System.Net.Http.Json;

namespace MagazziniMaterialiCLient.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/login", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                return result.Token;
            }
            throw new Exception("Login failed");
        }

        public async Task<string> RegistraOperatore(RegistraOperatoreDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/registra-operatore", dto);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<RegistrationResult>();
            return result.OperatoreId;
        }

        public async Task AssegnaRuoloOperatore(string userId)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/assegna-ruolo-operatore", userId);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddRole(string role)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/add-role", role);
            response.EnsureSuccessStatusCode();
        }

        public async Task AssignRole(UserRole model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/assign-role", model);
            response.EnsureSuccessStatusCode();
        }
    }
}