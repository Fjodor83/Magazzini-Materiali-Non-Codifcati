using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI.Mapper
{
    public interface IGiacenzaMapper
    {
        public Giacenza MapToMagazzino(GiacenzaDTO GiacenzaDTO);
        public GiacenzaDTO MapToMagazzinoDTO(Giacenza Magazzino);
    }
}
