using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IGiacenzaRepository
    {
        Giacenza GetGiacenza(int magazzinoId, string codiceMateriale);
        void AggiornaGiacenza(int magazzinoId, string codiceMateriale, int quantita);
    }

}
