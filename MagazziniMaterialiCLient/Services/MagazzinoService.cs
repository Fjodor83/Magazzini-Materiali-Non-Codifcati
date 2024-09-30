using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using MagazziniMaterialiCLient.Models.Entity.DTOs;

namespace MagazziniMaterialiCLient.Services
{
    public class MagazzinoService
    {
        private readonly HttpClient _httpClient;

        public MagazzinoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MagazzinoDTO>> GetMagazziniAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MagazzinoDTO>>("api/Magazzino");
        }

        public async Task<MagazzinoDTO> GetMagazzinoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<MagazzinoDTO>($"api/Magazzino/{id}");
        }

        public async Task<HttpResponseMessage> AddMagazzinoAsync(MagazzinoDTO newMagazzino)
        {
            return await _httpClient.PostAsJsonAsync("api/Magazzino", newMagazzino);
        }

        public async Task<HttpResponseMessage> EditMagazzinoAsync(int id, MagazzinoDTO magazzino)
        {
            return await _httpClient.PutAsJsonAsync($"api/Magazzino/{id}", magazzino);
        }

        public async Task<HttpResponseMessage> DeleteMagazzinoAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/Magazzino/{id}");
        }
    }
}