namespace MagazziniMaterialiCLient.Models
{
    public class MaterialeImmagine
    {

        public int Id { get; set; }
        public required string UrlImmagine { get; set; }
        public bool IsPrincipale { get; set; }
        public string QRCodeData { get; set; } = string.Empty;
    }
}
