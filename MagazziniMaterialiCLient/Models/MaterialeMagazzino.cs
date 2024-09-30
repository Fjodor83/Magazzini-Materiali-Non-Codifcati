namespace MagazziniMaterialiCLient.Models
{
    public class MaterialeMagazzino
    {
        public int Id { get; set; }
        public int MaterialeId { get; set; }
        public Materiale Materiale { get; set; }
        public int MagazzinoId { get; set; }
        public Magazzino Magazzino { get; set; }
        public int Quantita { get; set; }
    }
}
