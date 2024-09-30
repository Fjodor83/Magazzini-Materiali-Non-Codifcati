namespace MagazziniMaterialiCLient.Models.Entity.DTOs
{
    public class MaterialeDTO
    {
        public int Id { get; set; }
        public string CodiceMateriale { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
        public ICollection<MaterialeImmagine> Immagini { get; set; }
        public ICollection<Classificazione> Classificazioni { get; set; }
        public DateTime DataCreazione { get; set; }

        public MaterialeDTO()
        {
        }
    }
}