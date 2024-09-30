using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;


namespace MagazziniMaterialiAPI.Services
{
    public interface IMaterialiService
    {
        public List<MaterialeDTO> GetAll();
        public bool EditMateriale(string codiceMateriale, MaterialeDTO MaterialeDTO);
        public bool DeleteMateriale(string codiceMateriale);
        public Materiale AddMateriale(MaterialeDTO Materiale);
        public List<MagazzinoDTO> GetMagazziniByMaterialeId(string codiceMateriale);
        public void SaveChanges();
        bool ExistsByCodice(string codiceMateriale);
        MaterialeDTO? GetByCodiceMateriale(string codiceMateriale);
    }
}
