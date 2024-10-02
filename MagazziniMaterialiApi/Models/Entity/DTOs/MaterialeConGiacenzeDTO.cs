namespace MagazziniMaterialiAPI.Models.Entity.DTOs
{
    public class MaterialeConGiacenzeDTO : MaterialeDTO
    {
        public required List<GiacenzaDTO> Giacenze { get; set; }
    }
}
