namespace MagazziniMaterialiAPI.Models.Entity.DTOs
{
    public class MagazzinoConGiacenzeDTO : MagazzinoDTO
    {
        public required List<GiacenzaDTO> Giacenze { get; set; }
    }
}
