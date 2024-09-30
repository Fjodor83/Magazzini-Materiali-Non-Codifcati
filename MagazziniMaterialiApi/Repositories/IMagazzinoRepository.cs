
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Data;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMagazzinoRepository : IBaseRepository
    {
        public List<Magazzino> GetAll();
        public Magazzino? GetById(int id);

        public Magazzino AddMagazzino(Magazzino Magazzino);
        bool EditMagazzino(int MagazzinoId, Magazzino Magazzino);
        public void DeleteMagazzino(Magazzino Magazzino);
        List<Materiale> GetMaterialiByMagazzinoId(int MagazzinoId);
    }
}
