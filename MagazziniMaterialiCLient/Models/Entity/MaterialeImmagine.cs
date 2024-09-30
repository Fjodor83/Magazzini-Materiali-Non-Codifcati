using System.ComponentModel.DataAnnotations.Schema;

namespace MagazziniMaterialiCLient.Models.Entity
{
    public class MaterialeImmagine
    {

        public int Id { get; set; }
        public required string UrlImmagine { get; set; }
        public bool IsPrincipale { get; set; }
        public string QRCodeData { get; set; } = string.Empty;
    }
}
