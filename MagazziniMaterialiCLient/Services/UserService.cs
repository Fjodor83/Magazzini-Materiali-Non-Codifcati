using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using MagazziniMaterialiCLient.Models.Entity.DTOs;
using MagazziniMaterialiCLient.Models;

namespace MagazziniMaterialiCLient.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            // Chiama l'API per ottenere la lista degli utenti
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/account/get-users");
        }
        public async Task DeleteUserAsync(string userName)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/account/delete/{userName}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                throw;
            }
        }
        public async Task<List<string>> GetUserRolesAsync(string userName)
        {
            var response = await _httpClient.GetAsync($"api/account/get-user-role/{userName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<string>>();
        }


    }
}