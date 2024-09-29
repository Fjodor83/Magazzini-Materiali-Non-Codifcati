namespace MagazziniMaterialiCLient.Models
{
    public class MissionePrelievo
    {
        public int Id { get; set; }
        public string CodiceUnivoco { get; set; }
        public string TipologiaDestinazione { get; set; }
        public string Descrizione { get; set; }
        public string Stato { get; set; }
        public string OperatoreId { get; set; }
        public List<DettaglioMissione> DettagliMissione { get; set; }

        public MissionePrelievo()
        {
            DettagliMissione = new List<DettaglioMissione>();
        }
    }
}
