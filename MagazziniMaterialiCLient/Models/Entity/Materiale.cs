namespace MagazziniMaterialiCLient.Models.Entity
{
    public class Materiale
    {
        public int Id { get; set; }
        public string CodiceMateriale { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
        public DateTime DataCreazione { get; set; }
        public ICollection<MaterialeImmagine> Immagini { get; set; }
        public ICollection<Classificazione> Classificazioni { get; set; }
    }
}
