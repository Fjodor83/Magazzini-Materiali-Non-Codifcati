using System.Linq;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI
{
    public class MaterialeMapper : IMaterialeMapper
    {
        /// <summary>
        /// Maps MaterialeDTO to Materiale
        /// </summary>
        /// <param name="materialeDTO">The DTO to map from</param>
        /// <returns>A new Materiale instance</returns>
        public Materiale MapToMateriale(MaterialeDTO materialeDTO)
        {
            if (materialeDTO == null)
                return null;

            return new Materiale
            {
                CodiceMateriale = materialeDTO.CodiceMateriale,
                Descrizione = materialeDTO.Descrizione,
                DataCreazione = materialeDTO.DataCreazione,
                Note = materialeDTO.Note,
                Immagini = materialeDTO.Immagini?.Select(MapToMaterialeImmagine).ToList(),
                Classificazioni = materialeDTO.Classificazioni?.Select(MapToClassificazione).ToList()
            };
        }

        /// <summary>
        /// Maps Materiale to MaterialeDTO
        /// </summary>
        /// <param name="materiale">The entity to map from</param>
        /// <returns>A new MaterialeDTO instance</returns>
        public MaterialeDTO MapToMaterialeDTO(Materiale materiale)
        {
            if (materiale == null)
                return null;

            return new MaterialeDTO
            {
                CodiceMateriale = materiale.CodiceMateriale,
                Descrizione = materiale.Descrizione,
                Note = materiale.Note,
                DataCreazione = materiale.DataCreazione,
                Immagini = materiale.Immagini?.Select(MapToMaterialeImmagineDTO).ToList(),
                Classificazioni = materiale.Classificazioni?.Select(MapToClassificazioneDTO).ToList()
            };
        }

        private MaterialeImmagine MapToMaterialeImmagine(MaterialeImmagineDTO dto)
        {
            return new MaterialeImmagine
            {
                UrlImmagine = dto.UrlImmagine,
                IsPrincipale = dto.IsPrincipale,
                QRCodeData = dto.QRCodeData
            };
        }

        private MaterialeImmagineDTO MapToMaterialeImmagineDTO(MaterialeImmagine entity)
        {
            return new MaterialeImmagineDTO
            {
                UrlImmagine = entity.UrlImmagine,
                IsPrincipale = entity.IsPrincipale,
                QRCodeData = entity.QRCodeData
            };
        }

        private Classificazione MapToClassificazione(ClassificazioneDTO dto)
        {
            return new Classificazione
            {
                CodiceClassificazione = dto.CodiceClassificazione,
                NomeClassificazione = dto.NomeClassificazione
            };
        }

        private ClassificazioneDTO MapToClassificazioneDTO(Classificazione entity)
        {
            return new ClassificazioneDTO
            {
                CodiceClassificazione = entity.CodiceClassificazione,
                NomeClassificazione = entity.NomeClassificazione
            };
        }
    }
}