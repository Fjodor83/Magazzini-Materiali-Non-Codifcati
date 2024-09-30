namespace MagazziniMaterialiCLient.Models
{
    public class DettaglioMissione
    {
        public int Id { get; set; }
        public int MissionePrelievoId { get; set; }
        public int MaterialeId { get; set; }
        public Materiale Materiale { get; set; }
        public int QuantitaPrelevata { get; set; }
    }
}
