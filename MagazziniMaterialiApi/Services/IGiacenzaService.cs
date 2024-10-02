using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI.Services
{
    public interface IGiacenzaService
    {
        Task<GiacenzaDTO> GetByIdAsync(int id);
        Task<IEnumerable<GiacenzaDTO>> GetAllAsync();
        Task<IEnumerable<GiacenzaDTO>> GetByMagazzinoIdAsync(int magazzinoId);
        Task<IEnumerable<GiacenzaDTO>> GetByCodiceMaterialeAsync(string codiceMateriale);
        Task<GiacenzaDTO> GetByMagazzinoAndMaterialeAsync(int magazzinoId, string codiceMateriale);
        Task AddAsync(GiacenzaDTO giacenzaDto);
        Task UpdateAsync(GiacenzaDTO giacenzaDto);
        Task DeleteAsync(int id);
        Task AggiornaQuantitaAsync(int id, int nuovaQuantitaDisponibile, int nuovaQuantitaImpegnata);
    }
}
