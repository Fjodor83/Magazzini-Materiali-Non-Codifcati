using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI
{
    public class MagazzinoMapper : IMagazzinoMapper
    {
        /// <summary>
        /// map MagazzinoDTO to Magazzino
        /// </summary>
        /// <param name="MagazzinoDTO"></param>
        /// <returns></returns>
        public Magazzino MapToMagazzino(MagazzinoDTO MagazzinoDTO)
        {
            Magazzino Magazzino = new()
            {
                Id = MagazzinoDTO.Id,
                CodiceMagazzino = MagazzinoDTO.CodiceMagazzino,
                NomeMagazzino = MagazzinoDTO.NomeMagazzino,
                DescrizioneMagazzino = MagazzinoDTO.DescrizioneMagazzino,
                Note = MagazzinoDTO.Note
            };

            return Magazzino;

        }

        /// <summary>
        /// map Magazzino to MagazzinoDTO
        /// </summary>
        /// <param name="Magazzino"></param>
        /// <returns></returns>
        public MagazzinoDTO MapToMagazzinoDTO(Magazzino Magazzino)
        {
            MagazzinoDTO MagazzinoDTO = new()
            {
                Id = Magazzino.Id,
                NomeMagazzino = Magazzino.NomeMagazzino,
                CodiceMagazzino = Magazzino.CodiceMagazzino,
                DescrizioneMagazzino = Magazzino.DescrizioneMagazzino,
                Note = Magazzino.Note
            };


            return MagazzinoDTO;

        }
    }
}