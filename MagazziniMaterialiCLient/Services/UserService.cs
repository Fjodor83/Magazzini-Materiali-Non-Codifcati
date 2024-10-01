using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using MagazziniMaterialiCLient.Models.Entity.DTOs;

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
    }
}