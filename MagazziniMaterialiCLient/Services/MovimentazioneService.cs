using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MagazziniMaterialiCLient.Models.Entity.DTOs;

namespace MagazziniMaterialiCLient.Services
{
    public class MovimentazioneService
    {
        private readonly HttpClient _httpClient;

        public MovimentazioneService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> MovimentazioneIngresso(MovimentazioneDTO movimentazione)
        {
            return await _httpClient.PostAsJsonAsync("api/movimentazione/ingresso", movimentazione);
        }

        public async Task<HttpResponseMessage> MovimentazioneUscita(MovimentazioneDTO movimentazione)
        {
            return await _httpClient.PostAsJsonAsync("api/movimentazione/uscita", movimentazione);
        }

        public async Task<HttpResponseMessage> StornaMovimentazione(int id)
        {
            return await _httpClient.DeleteAsync($"api/movimentazione/{id}/storno");
        }
        public async Task<List<MovimentazioneDTO>> GetAllMovimentazioni()
        {
            return await _httpClient.GetFromJsonAsync<List<MovimentazioneDTO>>("api/movimentazioni");
        }
    }
}