using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI.Mapper
{
    public class GiacenzaMapper : IGiacenzaMapper
    {
        public Giacenza MapToMagazzino(GiacenzaDTO giacenzaDTO)
        {
            if (giacenzaDTO == null)
                return null;

            return new Giacenza
            {
                Id = giacenzaDTO.Id,
                MagazzinoId = giacenzaDTO.MagazzinoId,
                CodiceMateriale = giacenzaDTO.CodiceMateriale,
                QuantitaDisponibile = giacenzaDTO.QuantitaDisponibile,
                QuantitaImpegnata = giacenzaDTO.QuantitaImpegnata
            };
        }

        public GiacenzaDTO MapToMagazzinoDTO(Giacenza giacenza)
        {
            if (giacenza == null)
                return null;

            return new GiacenzaDTO
            {
                Id = giacenza.Id,
                MagazzinoId = giacenza.MagazzinoId,
                CodiceMateriale = giacenza.CodiceMateriale,
                QuantitaDisponibile = giacenza.QuantitaDisponibile,
                QuantitaImpegnata = giacenza.QuantitaImpegnata,
            };
        }
    }
}
