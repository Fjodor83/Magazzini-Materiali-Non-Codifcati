namespace MagazziniMaterialiAPI.Models.Entity.DTOs
{
    public class GiacenzaDTO
    {
        public int Id { get; set; }
        public string CodiceMateriale { get; set; }
        public int MagazzinoId { get; set; }
        public int QuantitaDisponibile { get; set; }
        public int QuantitaImpegnata { get; set; }
    }
}
