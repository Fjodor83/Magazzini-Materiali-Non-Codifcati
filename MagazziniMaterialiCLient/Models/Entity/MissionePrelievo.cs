using Microsoft.AspNet.Identity.EntityFramework;

using System.ComponentModel.DataAnnotations.Schema;

namespace MagazziniMaterialiCLient.Models.Entity
{
    public class MissionePrelievo
    {
        public int Id { get; set; }
        public string CodiceUnivoco { get; set; }
        public string TipologiaDestinazione { get; set; }
        public string Descrizione { get; set; }
        public string Stato { get; set; } // "Attiva", "Sospesa", "Completata"
        public string OperatoreId { get; set; }

        [ForeignKey("OperatoreId")]
        public IdentityUser Operatore { get; set; }
        public List<DettaglioMissione> DettagliMissione { get; set; }
    }

    public class DettaglioMissione
    {
        public int Id { get; set; }
        public int MissionePrelievoId { get; set; }
        public MissionePrelievo MissionePrelievo { get; set; }
        public int MaterialeId { get; set; }
        public Materiale Materiale { get; set; }
        public int QuantitaPrelevata { get; set; }
    }
}
