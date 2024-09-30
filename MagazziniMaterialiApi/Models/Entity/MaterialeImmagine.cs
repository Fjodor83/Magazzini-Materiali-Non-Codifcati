using System.ComponentModel.DataAnnotations.Schema;

namespace MagazziniMaterialiAPI.Models.Entity
{
    public class MaterialeImmagine
    {
        public int Id { get; set; }
        public string UrlImmagine { get; set; }
        public bool IsPrincipale { get; set; }
        public string QRCodeData { get; set; }

        [ForeignKey("Materiale")]
        public int MaterialeId { get; set; } // Non nullable se necessario
        public Materiale Materiale { get; set; }
    }
}
