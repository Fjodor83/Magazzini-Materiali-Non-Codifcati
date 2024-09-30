using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMaterialeRepository : IBaseRepository
    {
        public List<Materiale> GetAll();

        public Materiale AddMateriale(Materiale Materiale);
        bool EditMateriale(string codiceMateriale, Materiale Materiale);
        public void DeleteMateriale(Materiale Materiale);
        public List<Magazzino> GetMagazziniByMaterialeId(string codiceMateriale);
        bool ExistsByCodice(string codiceMateriale);
        Materiale? GetByCodiceMateriale(string codiceMateriale);
    }
}
