namespace MagazziniMaterialiCLient.Models.Entity.DTOs
{
    public class MovimentazioneDTO
    {
        public int Id { get; set; }
        public string CodiceMateriale { get; set; }
        public int MagazzinoId { get; set; }
        public int Quantita { get; set; }
        public string TipoMovimentazione { get; set; } // "Ingresso" o "Uscita"
        public DateTime DataMovimentazione { get; set; }
    }
}
