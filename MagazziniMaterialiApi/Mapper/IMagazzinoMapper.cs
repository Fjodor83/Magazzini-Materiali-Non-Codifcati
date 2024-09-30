

using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI
{
    public interface IMagazzinoMapper
    {
        public Magazzino MapToMagazzino(MagazzinoDTO MagazzinoDTO);
        public MagazzinoDTO MapToMagazzinoDTO(Magazzino Magazzino);

    }
}