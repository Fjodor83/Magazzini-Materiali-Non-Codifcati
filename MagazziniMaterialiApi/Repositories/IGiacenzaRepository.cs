using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IGiacenzaRepository
    {
        Giacenza GetGiacenza(int magazzinoId, string codiceMateriale);
        void AggiornaGiacenza(int magazzinoId, string codiceMateriale, int quantita);
        Task<Giacenza> GetByIdAsync(int id);
        Task<IEnumerable<Giacenza>> GetAllAsync();
        Task<IEnumerable<Giacenza>> GetByMagazzinoIdAsync(int magazzinoId);
        Task<IEnumerable<Giacenza>> GetByCodiceMaterialeAsync(string codiceMateriale);
        Task<Giacenza> GetByMagazzinoAndMaterialeAsync(int magazzinoId, string codiceMateriale);
        Task AddAsync(Giacenza giacenza);
        Task UpdateAsync(Giacenza giacenza);
        Task DeleteAsync(int id);
    }

}
