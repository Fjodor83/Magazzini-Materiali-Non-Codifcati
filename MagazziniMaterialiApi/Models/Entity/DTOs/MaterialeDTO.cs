namespace MagazziniMaterialiAPI.Models.Entity.DTOs
{
    public class MaterialeDTO
    {

        public string CodiceMateriale { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
        public ICollection<MaterialeImmagineDTO> Immagini { get; set; }
        public ICollection<ClassificazioneDTO> Classificazioni { get; set; }
        public DateTime DataCreazione { get; set; }

        public MaterialeDTO()
        {
            Immagini = new List<MaterialeImmagineDTO>();
            Classificazioni = new List<ClassificazioneDTO>();
        }
    }
}