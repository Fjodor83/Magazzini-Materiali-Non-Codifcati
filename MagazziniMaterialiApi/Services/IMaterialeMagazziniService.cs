using MagazziniMaterialiAPI.Models.Entity;


namespace MagazziniMaterialiAPI.Services
{
    public interface IMaterialeMagazziniService
    {
        public bool DeleteMaterialeMagazzino(int MagazzinoId, string codiceMateriale);
        public MaterialeMagazzino AddMaterialeMagazzino(int MagazzinoId, string codiceMateriale);
        public void SaveChanges();

    }
}
