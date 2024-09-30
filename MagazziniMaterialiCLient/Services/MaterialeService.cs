using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using MagazziniMaterialiCLient.Models.Entity.DTOs;

namespace MagazziniMaterialiCLient.Services
{
    public class MaterialeService
    {
        private readonly HttpClient _httpClient;

        public MaterialeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MaterialeDTO>> GetMateriali()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<MaterialeDTO>>("api/materiale");
        }

        public async Task<MaterialeDTO> GetMaterialeById(int id)
        {
            return await _httpClient.GetFromJsonAsync<MaterialeDTO>($"api/materiale/{id}");
        }

        public async Task<MaterialeDTO> GetMaterialeByCodiceMateriale(string codiceMateriale)
        {
            return await _httpClient.GetFromJsonAsync<MaterialeDTO>($"api/materiale/{codiceMateriale}");
        }

        public async Task<HttpResponseMessage> AddMateriale(MaterialeDTO materiale)
        {
            return await _httpClient.PostAsJsonAsync("api/materiale", materiale);
        }

        public async Task<HttpResponseMessage> EditMateriale(int id, MaterialeDTO materiale)
        {
            return await _httpClient.PutAsJsonAsync($"api/materiale/{id}", materiale);
        }

        public async Task<HttpResponseMessage> DeleteMateriale(int id)
        {
            return await _httpClient.DeleteAsync($"api/materiale/{id}");
        }
    }
}