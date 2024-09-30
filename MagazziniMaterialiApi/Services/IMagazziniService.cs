using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI.Services
{
    public interface IMagazziniService
    {
        public List<MagazzinoDTO> GetAll();
        public MagazzinoDTO? GetById(int id);
        public bool EditMagazzino(int MagazzinoId, MagazzinoDTO MagazzinoDTO);
        public bool DeleteMagazzino(int id);
        public Magazzino AddMagazzino(MagazzinoDTO Magazzino);
        public List<MaterialeDTO> GetMaterialiByMagazzinoId(int MagazzinoId);
        public void SaveChanges();

    }
}
